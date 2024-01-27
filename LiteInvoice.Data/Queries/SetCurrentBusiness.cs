using Dapper.QX.Abstract;
using Dapper.QX.Interfaces;
using LiteInvoice.Data.Interfaces;

namespace LiteInvoice.Data.Queries;

public class SetCurrentBusiness : TestableQuery<int>, IUserQuery
{
	public SetCurrentBusiness() : base(
		"""
		UPDATE "AspNetUsers"AS "u" SET 
			"CurrentBusinessId"=@businessId		
		WHERE 
			"u"."UserId" = @userId AND
			"u"."CurrentBusinessId" IS NULL
		""")
	{
	}

	public int UserId { get; set; }
	public int BusinessId { get; set; }

	protected override IEnumerable<ITestableQuery> GetTestCasesInner()
	{
		yield return new SetCurrentBusiness() { UserId = -1, BusinessId = 1 };
	}
}
