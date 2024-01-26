using Dapper.Entities.Attributes;
using LiteInvoice.Data.Entities.Conventions;
using LiteInvoice.Data.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace LiteInvoice.Data.Entities;

public class Business : BaseTable, IUserTable
{
    [NotUpdated]
    public int UserId { get; set; } = default!;

    [MaxLength(50)]
    public string DisplayName { get; set; } = default!;

    [MaxLength(50)]
    public string? Website { get; set; } = default!;

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
}
