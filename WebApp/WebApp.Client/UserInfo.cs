using LiteInvoice.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WebApp.Client
{
	// Add properties to this class and update the server and client AuthenticationStateProviders
	// to expose more information about the authenticated user to the client.
	public class UserInfo
	{
		public required string HashedUserId { get; set; }
		public required string GuidId { get; set; }
		public required string Email { get; set; }		

		public bool IsValid => !string.IsNullOrWhiteSpace(GuidId);

		public static UserInfo FromPrincipal(ClaimsPrincipal principal, IdentityOptions identityOptions)
		{
			var guidId = principal.FindFirst(identityOptions.ClaimsIdentity.UserIdClaimType)?.Value;
			var email = principal.FindFirst(identityOptions.ClaimsIdentity.EmailClaimType)?.Value;
			var hashedIntId = principal.FindFirst(nameof(ApplicationUser.UserId))?.Value;

			return new UserInfo
			{
				HashedUserId = hashedIntId ?? string.Empty,
				GuidId = guidId ?? string.Empty,
				Email = email ?? string.Empty,
			};
		}
	}
}
