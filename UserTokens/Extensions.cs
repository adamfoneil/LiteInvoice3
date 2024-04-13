using Microsoft.Extensions.DependencyInjection;

namespace UserTokens;

public static class Extensions
{
	public static void AddUserTokens<TUser>(this IServiceCollection services)
	{
		services.AddSingleton<UserTokenManager<TUser>>();
	}	
}
