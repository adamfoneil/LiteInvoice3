using LiteInvoice.Entities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Refit;
using System.Net.Http.Headers;
using System.Net;
using WebApp;
using WebApp.Client;
using WebApp.Components;
using WebApp.Components.Account;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.Secret.json", optional: true);
builder.AddHashIds();

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents()
	.AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRadzenComponents();
//builder.Services.AddUserTokens<ApplicationUser>();
builder.Services.AddScoped<MessageHandler>();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, DbClaimsPrincipalFactory>();
builder.Services
	.AddHttpClient("ServerAPI", client => client.BaseAddress = new Uri("https://localhost:44331/"))
	.AddHttpMessageHandler<MessageHandler>();

builder.Services.AddScoped<ApiClient>();

builder.Services
	.AddRefitClient<IApiClient>()
	.ConfigureHttpClient(client => client.BaseAddress = new Uri("https://localhost:44331/"));

builder.Services.AddAuthentication(options =>
	{
		options.DefaultScheme = IdentityConstants.ApplicationScheme;
		options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
	})		
	.AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddDapperEntities(connectionString);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddSignInManager()	
	.AddDefaultTokenProviders();

//builder.Services.AddAuthorization(config => config.AddPolicy())

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode()
	.AddInteractiveWebAssemblyRenderMode()
	.AddAdditionalAssemblies(typeof(UserInfo).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.MapQueries();
app.MapCrudOperations();

app.Run();
