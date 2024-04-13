using LiteInvoice.Entities;
using System.Net.Http.Json;

namespace WebApp.Client;

public class ApiClient(IHttpClientFactory httpClientFactory)
{
	private readonly HttpClient _client = httpClientFactory.CreateClient("ServerAPI");

	public async Task<IEnumerable<Project>> GetMyProjectsAsync()
	{
		var response = await _client.GetAsync($"/api/Queries/MyProjects");
		response.EnsureSuccessStatusCode();
		return await response.Content.ReadFromJsonAsync<IEnumerable<Project>>() ?? [];
	}
}
