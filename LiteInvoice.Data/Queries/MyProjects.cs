using Dapper.QX.Abstract;
using Dapper.QX.Interfaces;
using LiteInvoice.Data.Entities;
using LiteInvoice.Data.Interfaces;

namespace LiteInvoice.Data.Queries;

public class MyProjects : TestableQuery<Project>, IUserQuery
{
	public MyProjects() : base(
		"""
		SELECT 
			"p".*
		FROM 
			"Projects" AS "p" 
			INNER JOIN "Customers" AS "c" ON "p"."CustomerId" = "c"."Id"
			INNER JOIN "AspNetUsers" AS "u" ON "c"."BusinessId" = "u"."CurrentBusinessId"
		WHERE
			"u"."UserId" = @userId {andWhere}
		""")
	{			
	}

	public int UserId { get; set; }
	public bool? IsActive { get; set; } = true;

	protected override IEnumerable<ITestableQuery> GetTestCasesInner()
	{
		yield return new MyProjects() { UserId = 1 };
	}
}
