using Dapper.Entities.Attributes;
using LiteInvoice.Data.Entities.Conventions;
using LiteInvoice.Data.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiteInvoice.Data.Entities;

public class Customer : BaseTable, IContactInfo
{
	[NotUpdated]
	public int BusinessId { get; set; }
	[MaxLength(100)]
	public string Name { get; set; } = default!;
	[MaxLength(50)]
	public string? Email { get; set; } = default!;
	[MaxLength(20)]
	public string? PhoneNumber { get; set; } = default!;
	[MaxLength(50)]
	public string? MailingAddress { get; set; } = default!;
	[MaxLength(30)]
	public string? City { get; set; } = default!;
	[MaxLength(2)]
	public string? State { get; set; } = default!;
	[MaxLength(20)]
	public string? PostalCode { get; set; } = default!;
	[MaxLength(50)]
	public string? Country { get; set; } = default!;
	[Column(TypeName = "decimal(5,2)")]
	public decimal HourlyRate { get; set; }
}
