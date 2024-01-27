using Dapper.Entities.Attributes;
using LiteInvoice.Data.Entities.Conventions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiteInvoice.Data.Entities;

/// <summary>
/// fixed price line item on an invoiced
/// </summary>
[Table("LineEntries")]
public class LineEntry : BaseTable
{
	[NotUpdated]
	public int ProjectId { get; set; }
	[MaxLength(255)]
	public string Description { get; set; } = default!;
	[Column(TypeName = "decimal(6,2)")]
	public decimal Amount { get; set; }
	public int? InvoiceId { get; set; }
}
