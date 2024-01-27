using LiteInvoice.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LiteInvoice.App.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
	public DbSet<Business> Businesses { get; set; }
	public DbSet<Project> Projects { get; set; }
	public DbSet<WorkEntry> WorkEntries { get; set; }
	public DbSet<Invoice> Invoices { get; set; }
	public DbSet<Customer> Customers { get; set; }	
	public DbSet<LineEntry> LineEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

		builder.Entity<Invoice>().HasAlternateKey(nameof(Invoice.BusinessId), nameof(Invoice.Number));		
    }
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
