using Dapper.QX.Abstract;
using Dapper.QX.Interfaces;
using LiteInvoice.Data.Interfaces;
using LiteInvoice.Entities;

namespace LiteInvoice.Server.Queries;

public class MyPendingLineEntries : TestableQuery<LineEntry>, IUserQuery
{
	public MyPendingLineEntries() : base(
		"""
		SELECT 
			le.*
		FROM 
			"LineEntries" AS le
			INNER JOIN "Projects" AS p ON le."ProjectId" = p."Id"
			INNER JOIN "Customers" AS c ON p."CustomerId" = c."Id"
			INNER JOIN "AspNetUsers" AS u ON u."CurrentBusinessId" = c."BusinessId"
		WHERE
			le."ProjectId" = @projectId AND
			le."InvoiceId" IS NULL AND
			u."UserId"=@userId		
		""")
	{
	}

	public int ProjectId { get; set; }
	public int UserId { get; set; }

	protected override IEnumerable<ITestableQuery> GetTestCasesInner()
	{
		yield return new MyPendingLineEntries() { UserId = 1, ProjectId = 1 };
	}
}
