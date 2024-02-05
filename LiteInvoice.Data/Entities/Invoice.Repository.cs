using Dapper;
using HashidsNet;
using LiteInvoice.Data.Entities.Conventions;
using System.Data;

namespace LiteInvoice.Data.Entities;

public class InvoiceRepository(DapperEntities dapperEntities, Hashids hashIds) : BaseRepository<Invoice>(dapperEntities)
{
	private readonly Hashids HashIds = hashIds;

	public async Task<Invoice> GetAsync(string hashId)
	{
		var invoiceId = HashIds.DecodeSingle(hashId);
		return await GetAsync(invoiceId);
	}

	public async Task<int> CreateIdAsync(int projectId)
	{
		int invoiceId = 0;
		await Database.DoTransactionAsync(async (cn, txn) =>
		{
			var amount = await cn.ExecuteScalarAsync<decimal>(
				"""
				SELECT 
					SUM(we."Hours" * we."HourlyRate")
				FROM
					"WorkEntries" we
				WHERE
					"ProjectId" = @projectId AND
					"InvoiceId" IS NULL
				""", new { projectId }, txn);

			amount += await cn.ExecuteScalarAsync<decimal>(
				"""
				SELECT
					SUM(le."Amount")
				FROM
					"LineEntries" le
				WHERE
					"ProjectId" = @projectId AND
					"InvoiceId" IS NULL
				""", new { projectId }, txn);

			invoiceId = await cn.ExecuteScalarAsync<int>(
				"""
				INSERT INTO "Invoices" (
					"BusinessId", "Number", "ProjectId", "Amount", "PaidAmount", "CreatedBy", "DateCreated"
				) SELECT
					b."Id", b."NextInvoiceNumber", p."Id", @amount, 0, @userName, @localTime
				FROM
					"Projects" p
					INNER JOIN "Customers" c ON p."CustomerId" = c."Id"
					INNER JOIN "Businesses" b ON c."BusinessId" = b."Id"
				WHERE
					p."Id" = @projectId
				RETURNING
					"Id"
				""", new { projectId, amount, Database.CurrentUser.UserName, Database.CurrentUser.LocalTime }, txn);

			await cn.ExecuteAsync(
				"""
				UPDATE "Businesses" SET
					"NextInvoiceNumber" = b."NextInvoiceNumber" + 1
				FROM
					"Projects" p
					INNER JOIN "Customers" c ON p."CustomerId" = c."Id"
					INNER JOIN "Businesses" b ON c."BusinessId" = b."Id"
				WHERE
					p."Id" = @projectId
				""", new { projectId }, txn);

			await cn.ExecuteAsync(
				"""
				UPDATE "WorkEntries" SET "InvoiceId" = @invoiceId WHERE "ProjectId" = @projectId AND "InvoiceId" IS NULL;
				UPDATE "LineEntries" SET "InvoiceId" = @invoiceId WHERE "ProjectId" = @projectId AND "InvoiceId" IS NULL;
				""", new { invoiceId, projectId }, txn);
		});

		return invoiceId;
	}

	public async Task<Invoice> CreateAsync(int projectId)
	{
		var invoiceId = await CreateIdAsync(projectId);
		return await GetAsync(invoiceId);
	}

	public async Task DeleteAsync(int invoiceId)
	{
		using var cn = Database.GetConnection();
		await cn.ExecuteAsync(
			"""
			UPDATE "WorkEntries" SET 
				"InvoiceId" = NULL
			FROM 
				"Invoices"
				INNER JOIN "Businesses" AS b ON "Invoices"."BusinessId" = b."Id"
				INNER JOIN "AspNetUsers" AS u ON b."UserId" = u."UserId"
			WHERE 				
				"WorkEntries"."InvoiceId" = @invoiceId AND 
				u."UserId" = @userId;
			
			UPDATE "LineEntries" SET
				"InvoiceId" = NULL
			FROM 
				"Invoices"
				INNER JOIN "Businesses" AS b ON "Invoices"."BusinessId" = b."Id"
				INNER JOIN "AspNetUsers" AS u ON b."UserId" = u."UserId"
			WHERE 				
				"LineEntries"."InvoiceId" = @invoiceId AND 
				u."UserId" = @userId;
						
			DELETE FROM "Invoices" AS i
			USING 
				"Businesses" AS b, "AspNetUsers" AS u
			WHERE
				i."BusinessId" = b."Id" AND
				b."UserId" = u."UserId" AND
				i."Id" = @invoiceId AND
				u."UserId" = @userId			
			""", new { invoiceId, Database.CurrentUser.UserId });
	}
}
