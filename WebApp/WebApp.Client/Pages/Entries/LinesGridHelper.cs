using AO.Radzen.Components.Abstract;
using LiteInvoice.Entities;
using Radzen;
using WebApp.Client;

namespace WebApp.Components.Pages.Entries;

internal class LineEntryGridHelper(string userName, DialogService dialogs, IApiClient client) : GridHelper<LineEntry>(dialogs)
{
	private readonly IApiClient Client = client;
	private readonly string UserName = userName;

	public int ProjectId { get; set; }

	public decimal Amount { get; private set; }

	protected override async Task OnRefreshAsync()
	{
		await Task.CompletedTask;
		Amount = Data.Sum(row => row.Amount);
	}

	public override async Task OnDeleteAsync(LineEntry row) => await Client.DeleteLineEntryAsync(UserName, row);

	public override async Task OnSaveAsync(LineEntry row) => await Client.SaveLineEntryAsync(UserName, row);

	public override async Task<IEnumerable<LineEntry>> QueryAsync() =>
		await Client.GetMyPendingLineEntries(UserName, ProjectId);            
}
