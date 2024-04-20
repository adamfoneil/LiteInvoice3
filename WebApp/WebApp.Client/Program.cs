using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using WebApp.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddRadzenComponents();

builder.Services
	.AddTransient<CookieHandler>()	
	.AddHttpClient(ApiClient.Name, client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))	
	.AddHttpMessageHandler<CookieHandler>();

builder.Services.AddScoped<ApiClient>();

await builder.Build().RunAsync();
