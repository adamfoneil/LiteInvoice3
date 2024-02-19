using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WebApp.Client
{
	// Add properties to this class and update the server and client AuthenticationStateProviders
	// to expose more information about the authenticated user to the client.
	public class UserInfo
	{
		public required string UserId { get; set; }
		public required string Email { get; set; }

		public bool IsValid => !string.IsNullOrWhiteSpace(UserId);

		public static UserInfo FromPrincipal(ClaimsPrincipal principal, IdentityOptions identityOptions)
		{
			var userId = principal.FindFirst(identityOptions.ClaimsIdentity.UserIdClaimType)?.Value;
			var email = principal.FindFirst(identityOptions.ClaimsIdentity.EmailClaimType)?.Value;

			return new UserInfo
			{
				UserId = userId ?? string.Empty,
				Email = email ?? string.Empty,
			};
		}
	}
}
