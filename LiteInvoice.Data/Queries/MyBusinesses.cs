using Dapper.QX.Abstract;
using Dapper.QX.Interfaces;
using LiteInvoice.Data.Entities;
using LiteInvoice.Data.Interfaces;

namespace LiteInvoice.Data.Queries;

public class MyBusinesses : TestableQuery<Business>, IUserQuery
{
	public MyBusinesses() : base(
		"""
		SELECT * FROM "Businesses" WHERE "UserId"=@userId {andWhere} ORDER BY "DisplayName"
		""")
	{	
	}

	public int UserId { get; set; }
	public bool? IsActive { get; set; } = true;

	protected override IEnumerable<ITestableQuery> GetTestCasesInner()
	{
		yield return new MyBusinesses() { UserId = 1 };
	}
}
