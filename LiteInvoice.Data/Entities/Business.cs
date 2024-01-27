using Dapper.Entities.Attributes;
using LiteInvoice.Data.Entities.Conventions;
using LiteInvoice.Data.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiteInvoice.Data.Entities;

[Table("Businesses")]
public class Business : BaseTable, IUserTable, IContactInfo
{
    [NotUpdated]
    public int UserId { get; set; } = default!;

    [MaxLength(50)]
    public string DisplayName { get; set; } = default!;

	[MaxLength(50)]
	public string? ContactName { get; set; } = default!;

    [MaxLength(50)]
    public string? Website { get; set; } = default!;

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

    [MaxLength(30)]
    public string? TaxId { get; set; } = default!;

    [Column(TypeName = "decimal(5,2)")]
    public decimal HourlyRate { get; set; }

    public int NextInvoiceNumber { get; set; } = 1000;

	public bool UsePayPalMe { get; set; }
	[MaxLength(100)]
	public string? PayPalMeLink { get; set; } = default!;
	
	public bool UseCashApp { get; set; }
	[MaxLength(100)]
	public string? CashAppLink { get; set; } = default!;

	public bool UseVenmo { get; set; }
	[MaxLength(100)]
	public string? VenmoLink { get; set; } = default!;

	public const string PayPalMeLinkPrefix = "https://paypal.me";
	public const string CashAppLinkPrefix = "https://cash.app";
	public const string VenmoLinkPrefix = "https://venmo.com";

	[NotMapped]
	public List<PaymentMethod> PaymentMethods { get; set; } = [];

	public List<PaymentMethod> GetPaymentMethods() =>
	[
		new PaymentMethod() { Name = "PayPal.Me", IsEnabled = UsePayPalMe, LinkPrefix = PayPalMeLinkPrefix, MyLink = PayPalMeLink, Setter = (enabled, link) => { UsePayPalMe = enabled; PayPalMeLink = link; } },
		new PaymentMethod() { Name = "Cash.App", IsEnabled = UseCashApp, LinkPrefix = CashAppLinkPrefix, MyLink = CashAppLink, Setter = (enabled, link) => { UseCashApp = enabled; CashAppLink = link; } },
		new PaymentMethod() { Name = "Venmo", IsEnabled = UseVenmo, LinkPrefix = VenmoLinkPrefix, MyLink = VenmoLink, Setter = (enabled, link) => { UseVenmo = enabled; VenmoLink = link; } }
	];	
	
	public class PaymentMethod
	{
		public string Name { get; init; } = default!;
		public string LinkPrefix { get; init; } = default!;
		public bool IsEnabled { get; set; }
		public string? MyLink { get; set; } = default!;
		public Action<bool, string?> Setter { get; init; } = default!;
	}
}