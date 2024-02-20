using Dapper.Entities.Attributes;
using LiteInvoice.Entities.Conventions;
using LiteInvoice.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiteInvoice.Entities;

[Table("Invoices")]
public class Invoice : BaseTable, IHashedResult
{
	[NotUpdated]
	public int BusinessId { get; set; }
	[NotUpdated]
	public int Number { get; set; }
	[NotUpdated]
	public int ProjectId { get; set; }
	[NotUpdated]
	[Column(TypeName = "decimal(6,2)")]
	public decimal Amount { get; set; }
	[Column(TypeName = "decimal(6,2)")]
	public decimal PaidAmount { get; set; }

	[NotMapped]
	public string CustomerName { get; set; } = default!;
	[NotMapped]
	public string ProjectName { get; set; } = default!;
	[NotMapped]
	public string HashedId { get; set; } = default!;
}
