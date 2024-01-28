using Dapper.QX.Abstract;
using Dapper.QX.Interfaces;
using LiteInvoice.Data.Entities;
using LiteInvoice.Data.Interfaces;

namespace LiteInvoice.Data.Queries;

public class MyPendingWorkEntries : TestableQuery<WorkEntry>, IUserQuery
{
	public MyPendingWorkEntries() : base(
		"""
		SELECT 
			we.*
		FROM 
			"WorkEntries" AS we
			INNER JOIN "Projects" AS p ON we."ProjectId" = p."Id"
			INNER JOIN "Customers" AS c ON p."CustomerId" = c."Id"
			INNER JOIN "AspNetUsers" AS u ON u."CurrentBusinessId" = c."BusinessId"
		WHERE
			we."ProjectId" = @projectId AND
			we."InvoiceId" IS NULL AND
			u."UserId"=@userId
		ORDER BY
			we."Date"
		""")
	{			
	}

	public int ProjectId { get; set; }
	public int UserId { get; set; }

	protected override IEnumerable<ITestableQuery> GetTestCasesInner()
	{
		yield return new MyPendingWorkEntries() {  UserId = 1, ProjectId = 1 };
	}
}
