using LiteInvoice.Entities.Static;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiteInvoice.Entities;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
	[MaxLength(50)]
	public string? TimeZoneId { get; set; }
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int UserId { get; set; }
	public int? CurrentBusinessId { get; set; }
	[NotMapped]
	public DateTime LocalTime => DateTimeHelper.Now(TimeZoneId);
}
