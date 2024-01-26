using LiteInvoice.Data.Entities.Conventions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiteInvoice.Data.Entities;

public class WorkEntry : BaseTable
{
    public int ProjectId { get; set; }
    public DateOnly Date { get; set; }
    [MaxLength(255)]
    public string Comments { get; set; } = default!;
    [Column(TypeName = "decimal(4,2)")]
    public decimal Hours { get; set; }
}
