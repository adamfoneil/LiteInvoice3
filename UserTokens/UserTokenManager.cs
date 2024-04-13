using Microsoft.AspNetCore.DataProtection;
using System.Security.Claims;
using System.Text.Json;

namespace UserTokens;

public class UserTokenManager<TUser>(IDataProtectionProvider dataProtectionProvider)
{
	private readonly IDataProtectionProvider _dataProtectionProvider = dataProtectionProvider;

	public string ClaimName => "SimpleUserToken";

	public string Encrypt(TUser user)
	{
		var json = JsonSerializer.Serialize(user);
		var protector = CreateProtector();
		return protector.Protect(json);
	}

	public TUser Decrypt(string token)
	{
		var protector = CreateProtector();
		var json = protector.Unprotect(token);
		return JsonSerializer.Deserialize<TUser>(json) ?? throw new Exception("Couldn't deserialize protected data");
	}

	public Claim GetClaim(TUser user) => new(ClaimName, Encrypt(user));

	private IDataProtector CreateProtector() => _dataProtectionProvider.CreateProtector(typeof(TUser).FullName);
}
