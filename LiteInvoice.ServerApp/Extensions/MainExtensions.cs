using HashidsNet;
using LiteInvoice.Data.Entities;

namespace LiteInvoice.App.Extensions;

internal static class MainExtensions
{
	internal static void AddDapperEntities(this IServiceCollection services, string connectionString)
	{
		services.AddScoped(sp =>
		{
			var hashIds = sp.GetRequiredService<Hashids>();
			DapperEntities result = new(connectionString, sp.GetRequiredService<ILogger<DapperEntities>>(), hashIds);
			
			var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
			var context = httpContextAccessor.HttpContext;
			result.IsLoggedIn = context?.User?.Identity?.IsAuthenticated ?? false;
			result.CurrentUserName = (result.IsLoggedIn) ? context!.User!.Identity!.Name! : DapperEntities.DefaultUserName;

			return result;
		});
	}

	internal static void AddHashIds(this WebApplicationBuilder appBuilder)
	{
		appBuilder.Services.AddSingleton(sp =>
		{
			var config = appBuilder.Configuration.GetSection("HashIds");
			var salt = config.GetValue<string>("Salt");
			int minLength = config.GetValue<int?>("MinLength") ?? 5;
			return new Hashids(salt, minLength);
		});
	}
}
