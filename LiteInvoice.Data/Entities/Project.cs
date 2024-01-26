using Dapper.Entities.Attributes;
using LiteInvoice.Data.Entities.Conventions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiteInvoice.Data.Entities;

public class Project : BaseTable
{
    [NotUpdated]
    public int BusinessId { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = default!;

    [MaxLength(255)]
    public string? Description { get; set; } = default!;

    [Column(TypeName = "decimal(5,2)")]
    public decimal? HourlyRate { get; set; }

    public bool IsActive { get; set; } = true;
}
