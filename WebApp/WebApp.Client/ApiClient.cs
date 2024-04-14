using ApiClientBaseLibrary;
using LiteInvoice.Entities;
using System.Runtime.CompilerServices;

namespace WebApp.Client;

public class ApiClient(IHttpClientFactory httpClientFactory, ILogger<ApiClient> logger) : ApiClientBase(httpClientFactory, logger)
{
	protected override HttpClient CreateClient(IHttpClientFactory httpClientFactory) => httpClientFactory.CreateClient("ServerAPI");

	public async Task<IEnumerable<Project>> GetMyProjectsAsync() => await GetAsync<IEnumerable<Project>>("/api/Queries/MyProjects") ?? [];

	protected override async Task<bool> HandleException(HttpResponseMessage? response, Exception exception, [CallerMemberName] string? methodName = null)
	{
		return await Task.FromResult(false);
	}
}
