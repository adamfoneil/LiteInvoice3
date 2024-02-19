using LiteInvoice.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WebApp.Client
{
	// Add properties to this class and update the server and client AuthenticationStateProviders
	// to expose more information about the authenticated user to the client.
	public class UserInfo
	{
		public required int UserId { get; set; }
		public required string GuidId { get; set; }
		public required string Email { get; set; }		

		public bool IsValid => !string.IsNullOrWhiteSpace(GuidId);

		public static UserInfo FromPrincipal(ClaimsPrincipal principal, IdentityOptions identityOptions)
		{
			var guidId = principal.FindFirst(identityOptions.ClaimsIdentity.UserIdClaimType)?.Value;
			var email = principal.FindFirst(identityOptions.ClaimsIdentity.EmailClaimType)?.Value;
			var dbId = int.Parse(principal.FindFirst(nameof(ApplicationUser.UserId))?.Value ?? "0");

			return new UserInfo
			{
				UserId = dbId,
				GuidId = guidId ?? string.Empty,
				Email = email ?? string.Empty,
			};
		}
	}
}
