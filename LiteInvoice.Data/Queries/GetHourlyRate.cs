using Dapper.QX.Abstract;
using Dapper.QX.Interfaces;
using LiteInvoice.Data.Entities;

namespace LiteInvoice.Data.Queries;

public class GetHourlyRateResult
{
	public RateSource RateSource { get; set; }
	public decimal Rate { get; set; }
}

public class GetHourlyRate : TestableQuery<GetHourlyRateResult>
{
	public GetHourlyRate() : base(
		@"SELECT
			COALESCE(""Projects"".""HourlyRate"", "")")
	{
	}

	public int ProjectId { get; set; }

	protected override IEnumerable<ITestableQuery> GetTestCasesInner()
	{
		throw new NotImplementedException();
	}
}
