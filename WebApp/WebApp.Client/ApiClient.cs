using ApiClientBaseLibrary;
using LiteInvoice.Entities;
using System.Runtime.CompilerServices;

namespace WebApp.Client;

public class ApiClient(IHttpClientFactory factory, ILogger<ApiClient> logger) : ApiClientBase(factory.CreateClient(Name), logger)
{
	public const string Name = "API";

	public async Task<IEnumerable<Project>> GetMyProjectsAsync() => await GetAsync<IEnumerable<Project>>("/api/Queries/MyProjects") ?? [];

	public async Task<IEnumerable<Customer>> GetMyCustomersAsync() => await GetAsync<IEnumerable<Customer>>("/api/Queries/MyCustomers") ?? [];

	public async Task<Invoice> CreateInvoiceAsync(int projectId) => await PostWithResultAsync<Invoice>($"/api/Invoice/{projectId}") ?? throw new Exception("Invoice not created");

	protected override async Task<bool> HandleException(HttpResponseMessage? response, Exception exception, [CallerMemberName] string? methodName = null)
	{
		return await Task.FromResult(false);
	}

	internal async Task DeleteLineEntryAsync(LineEntry row)
	{
		throw new NotImplementedException();
	}

	internal async Task SaveLineEntryAsync(LineEntry row)
	{
		throw new NotImplementedException();
	}

	internal async Task<IEnumerable<LineEntry>> GetMyPendingLineEntries(int projectId)
	{
		throw new NotImplementedException();
	}

	internal async Task DeleteWorkEntryAsync(WorkEntry row)
	{
		throw new NotImplementedException();
	}

	internal async Task SaveWorkEntryAsync(WorkEntry row)
	{
		throw new NotImplementedException();
	}

	internal async Task<IEnumerable<WorkEntry>> GetMyPendingWorkEntries(int projectId)
	{
		throw new NotImplementedException();
	}
}
