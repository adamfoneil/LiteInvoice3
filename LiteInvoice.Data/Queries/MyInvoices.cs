using Dapper.QX.Abstract;
using Dapper.QX.Interfaces;
using LiteInvoice.Data.Entities;
using LiteInvoice.Data.Interfaces;

namespace LiteInvoice.Data.Queries;

public class MyInvoices : TestableQuery<Invoice>, IUserQuery
{
	public MyInvoices() : base(
		"""

		""")
	{		
	}

	public int UserId { get; set; }

	protected override IEnumerable<ITestableQuery> GetTestCasesInner()
	{
		throw new NotImplementedException();
	}
}
