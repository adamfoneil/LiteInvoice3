using LiteInvoice.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace WebApp;

public class DbClaimsPrincipalFactory(
	UserManager<ApplicationUser> userManager,
	IOptions<IdentityOptions> optionsAccessor) : UserClaimsPrincipalFactory<ApplicationUser>(userManager, optionsAccessor)
{
	protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
	{
		var identity = await base.GenerateClaimsAsync(user);
		identity.AddClaim(new Claim(nameof(ApplicationUser.UserId), user.UserId.ToString()));		
		return identity;
	}
}
