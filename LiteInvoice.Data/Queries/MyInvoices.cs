using Dapper.QX.Abstract;
using Dapper.QX.Interfaces;
using LiteInvoice.Data.Entities;
using LiteInvoice.Data.Interfaces;

namespace LiteInvoice.Data.Queries;

public class MyInvoices : TestableQuery<Invoice>, IUserQuery
{
	public MyInvoices() : base(
		"""
		SELECT 
			inv.*, c."Name" AS "CustomerName", p."Name" AS "ProjectName"
		FROM
			"Invoices" AS inv
			INNER JOIN "Projects" AS p ON inv."ProjectId" = p."Id"
			INNER JOIN "Customers" AS c ON p."CustomerId" = c."Id"
			INNER JOIN "AspNetUsers" AS u ON c."BusinessId" = u."CurrentBusinessId"
		WHERE
			u."UserId" = @userId {andWhere}
		""")
	{		
	}

	public int UserId { get; set; }

	protected override IEnumerable<ITestableQuery> GetTestCasesInner()
	{
		yield return new MyInvoices() { UserId = 1 };
	}
}
