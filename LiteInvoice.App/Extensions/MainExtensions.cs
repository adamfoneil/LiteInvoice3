using LiteInvoice.Data.Entities;

namespace LiteInvoice.App.Extensions;

internal static class MainExtensions
{
	internal static void AddDapperEntities(this IServiceCollection services, string connectionString)
	{
		services.AddScoped(sp =>
		{
			DapperEntities result = new(connectionString, sp.GetRequiredService<ILogger<DapperEntities>>());
			
			var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
			var context = httpContextAccessor.HttpContext;
			result.IsLoggedIn = context?.User?.Identity?.IsAuthenticated ?? false;
			result.CurrentUserName = (result.IsLoggedIn) ? context!.User!.Identity!.Name! : DapperEntities.DefaultUserName;

			return result;
		});
	}
}
