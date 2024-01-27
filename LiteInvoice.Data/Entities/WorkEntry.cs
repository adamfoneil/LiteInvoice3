using Dapper.Entities;
using Dapper.Entities.Attributes;
using LiteInvoice.Data.Entities.Conventions;
using LiteInvoice.Data.Queries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace LiteInvoice.Data.Entities;

public enum RateSource
{
	Project = 1,
	Customer = 2,
	Business = 3
}

public class WorkEntry : BaseTable
{
	[NotUpdated]
	public int ProjectId { get; set; }
	public DateOnly Date { get; set; }
	[MaxLength(255)]
	public string Comments { get; set; } = default!;
	[Column(TypeName = "decimal(4,2)")]
	public decimal Hours { get; set; }
	public RateSource RateSource { get; set; }
    [Column(TypeName = "decimal(5,2)")]
    public decimal HourlyRate { get; set; }
	public decimal Amount => Hours * HourlyRate;
	public int? InvoiceId { get; set; }
}

public class WorkEntryRepository(DapperEntities database) : BaseRepository<WorkEntry>(database)
{
	protected override async Task BeforeSaveAsync(IDbConnection connection, RepositoryAction action, WorkEntry entity, IDbTransaction? transaction)
	{
		var rateInfo = await new GetHourlyRate() { ProjectId = entity.ProjectId }.ExecuteSingleAsync(connection);
		entity.RateSource = rateInfo.EffectiveRate.Source;
		entity.HourlyRate = rateInfo.EffectiveRate.Rate;

		await base.BeforeSaveAsync(connection, action, entity, transaction);
	}
}
