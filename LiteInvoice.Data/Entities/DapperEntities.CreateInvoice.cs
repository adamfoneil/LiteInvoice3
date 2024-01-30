using Dapper;

namespace LiteInvoice.Data.Entities;

public partial class DapperEntities
{
	public async Task<int> CreateInvoiceAsync(int projectId)
	{
		int invoiceId = 0;
		await DoTransactionAsync(async (cn, txn) =>
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
					"BusinessId", "Number", "Amount", "PaidAmount", "CreatedBy", "DateCreated"
				) SELECT
					b."Id", b."NextInvoiceNumber", @amount, 0, @userName, @localTime
				FROM
					"Projects" p
					INNER JOIN "Customers" c ON p."CustomerId" = c."Id"
					INNER JOIN "Businesses" b ON c."BusinessId" = b."Id"
				WHERE
					p."Id" = @projectId
				RETURNING
					"Id"
				""", new { projectId, amount, CurrentUser.UserName, CurrentUser.LocalTime }, txn);

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
}
