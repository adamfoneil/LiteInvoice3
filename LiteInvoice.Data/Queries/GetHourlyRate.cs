using Dapper.QX.Abstract;
using Dapper.QX.Interfaces;
using LiteInvoice.Data.Entities;

namespace LiteInvoice.Data.Queries;

public class GetHourlyRateResult
{
	public RateSource? ProjectRateSource { get; set; }
	public decimal? ProjectRate { get; set; }
	public RateSource? CustomerRateSource { get; set; }
	public decimal? CustomerRate { get; set; }
	public RateSource BusinessRateSource { get; set; }
	public decimal BusinessRate { get; set; }

	private IEnumerable<(RateSource?, decimal?)> AllRates =>
	[
		(ProjectRateSource, ProjectRate),
		(CustomerRateSource, CustomerRate),
		(BusinessRateSource, BusinessRate),
	];

	public (RateSource Source, decimal Rate) EffectiveRate
	{
		get
		{
			var (source, rate) = AllRates.First(item => item.Item1.HasValue);
			return (source!.Value, rate!.Value);
		}		
	}	
}

public class GetHourlyRate : TestableQuery<GetHourlyRateResult>
{
	public GetHourlyRate() : base(
		"""
		  SELECT
			1 AS "ProjectRateSource", "prj"."HourlyRate" AS "ProjectRate",
			2 AS "CustomerRateSource", "c"."HourlyRate" AS "CustomerRate",
			3 AS "BusinessRateSource", "b"."HourlyRate" AS "BusinessRate"
		FROM
			"Projects" AS "prj"
			INNER JOIN "Customers" AS "c" ON "prj"."CustomerId" = "c"."Id"
			INNER JOIN "Businesses" AS "b" ON "c"."BusinessId" = "b"."Id"
		WHERE
			"prj"."Id" = @projectId
		""")
	{
	}

	public int ProjectId { get; set; }

	protected override IEnumerable<ITestableQuery> GetTestCasesInner()
	{
		yield return new GetHourlyRate() { ProjectId = 1 };
	}
}
