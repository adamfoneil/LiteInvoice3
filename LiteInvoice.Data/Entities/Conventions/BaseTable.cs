using Dapper.Entities.Attributes;
using Dapper.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace LiteInvoice.Data.Entities.Conventions;

public abstract class BaseTable : IEntity<int>
{
	public int Id { get; set; }

	[MaxLength(50)]
	[NotUpdated]
	public string CreatedBy { get; set; } = default!;
	[NotUpdated]
	public DateTime DateCreated { get; set; }
	[NotInserted]
	[MaxLength(50)]
	public string? ModifiedBy { get; set; }
	[NotInserted]
	public DateTime? DateModified { get; set; }
}
