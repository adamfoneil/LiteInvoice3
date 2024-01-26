using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LiteInvoice.App.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{	
}

public class AppDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
	private static IConfiguration Config => new ConfigurationBuilder()
		.AddJsonFile("appsettings.Secret.json", optional: false)
		.Build();

	public ApplicationDbContext CreateDbContext(string[] args)
	{
		var connectionString = Config.GetConnectionString("DefaultConnection");
		var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
		builder.UseNpgsql(connectionString);
		return new ApplicationDbContext(builder.Options);
	}
}
