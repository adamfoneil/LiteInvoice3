using Dapper;

namespace LiteInvoice.Data.Entities;

public partial class DapperEntities
{
	public async Task DeleteInvoiceAsync(int invoiceId)
	{
		using var cn = GetConnection();
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
			""", new { invoiceId, CurrentUser.UserId });
	}
}
