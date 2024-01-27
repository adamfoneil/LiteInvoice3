using Dapper.QX.Abstract;
using Dapper.QX.Interfaces;
using LiteInvoice.Data.Entities;
using LiteInvoice.Data.Interfaces;

namespace LiteInvoice.Data.Queries;

public class MyCustomers : TestableQuery<Customer>, IUserQuery
{
	public MyCustomers() : base(
		"""
		SELECT 
			"c".* 
		FROM 
			"Customers" AS "c" 			
			INNER JOIN "AspNetUsers" AS "u" ON "c"."BusinessId" = "u"."CurrentBusinessId"
		WHERE
			"u"."UserId" = @userId
		ORDER BY 
			"c"."Name"
		""")
	{			
	}

	public int UserId { get; set; }

	protected override IEnumerable<ITestableQuery> GetTestCasesInner()
	{
		yield return new MyCustomers() { UserId = 1 };
	}
}
