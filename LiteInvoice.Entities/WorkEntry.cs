using Dapper.Entities.Attributes;
using LiteInvoice.Entities.Conventions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiteInvoice.Entities;

[Table("WorkEntries")]
public class WorkEntry : BaseTable
{
	[NotUpdated]
	public int ProjectId { get; set; }
	[Column(TypeName = "date")]
	public DateTime Date { get; set; }
	[MaxLength(255)]
	public string Comments { get; set; } = default!;
	[Column(TypeName = "decimal(4,2)")]
	public decimal Hours { get; set; }
	[Column(TypeName = "decimal(5,2)")]
	public decimal HourlyRate { get; set; }
	[NotMapped]
	public decimal Amount => Hours * HourlyRate;
	public int? InvoiceId { get; set; }

	public Project? Project { get; private set; }
}