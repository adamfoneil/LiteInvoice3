using AO.Radzen.Components.Abstract;
using LiteInvoice.Entities;
using Radzen;
using WebApp.Client;

namespace WebApp.Components.Pages.Entries;

internal class WorkEntryGridHelper(string userName, DialogService dialog, IApiClient client) : GridHelper<WorkEntry>(dialog)
{    
    private readonly IApiClient Client = client;
    private readonly string UserName = userName;

    public int CustomerId { get; set; }
    public int ProjectId { get; set; }

    public decimal HourlyAmount { get; private set; }
    public decimal TotalHours { get; private set; }
    public string AccordionText => $"Hours - {TotalHours} hrs | {HourlyAmount:c2}";

    public override async Task OnDeleteAsync(WorkEntry row) => await Client.DeleteWorkEntryAsync(UserName, row);

    public override async Task OnSaveAsync(WorkEntry row) => await Client.SaveWorkEntryAsync(UserName, row);

    protected override async Task OnRefreshAsync()
    {
        await Task.CompletedTask;
        HourlyAmount = Data.Sum(row => row.Amount);
        TotalHours = Data.Sum(row => row.Hours);
    }

    public override async Task<IEnumerable<WorkEntry>> QueryAsync() =>
        await Client.GetMyPendingWorkEntries(UserName, ProjectId);
}
