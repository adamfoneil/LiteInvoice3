using Dapper.Entities.Attributes;
using LiteInvoice.Data.Entities.Conventions;

namespace LiteInvoice.Data.Entities;

public class Invoice : BaseTable
{
    [NotUpdated]
    public int BusinessId { get; set; }
    [NotUpdated]
    public int Number { get; set; }
    [NotUpdated]
    public decimal Amount { get; set; }
}
