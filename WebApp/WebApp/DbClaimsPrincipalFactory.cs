using HashidsNet;
using LiteInvoice.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace WebApp;

public class DbClaimsPrincipalFactory(
	UserManager<ApplicationUser> userManager,
	IOptions<IdentityOptions> optionsAccessor,
	Hashids hashids) : UserClaimsPrincipalFactory<ApplicationUser>(userManager, optionsAccessor)
{
	private readonly Hashids Hashids = hashids;

	protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
	{
		var identity = await base.GenerateClaimsAsync(user);
		identity.AddClaim(new Claim(nameof(ApplicationUser.UserId), Hashids.Encode(user.UserId)));
		return identity;
	}
}
