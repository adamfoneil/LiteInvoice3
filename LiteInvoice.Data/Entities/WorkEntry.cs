using Dapper.Entities;
using Dapper.Entities.Attributes;
using LiteInvoice.Data.Entities.Conventions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace LiteInvoice.Data.Entities;

public class WorkEntry : BaseTable
{
	[NotUpdated]
	public int ProjectId { get; set; }
	public DateOnly Date { get; set; }
	[MaxLength(255)]
	public string Comments { get; set; } = default!;
	[Column(TypeName = "decimal(4,2)")]
	public decimal Hours { get; set; }
    [Column(TypeName = "decimal(5,2)")]
    public decimal HourlyRate { get; set; }
	public decimal Amount => Hours * HourlyRate;
	public int? InvoiceId { get; set; }
}