using AO.Radzen.Components.Abstract;
using LiteInvoice.Data.Entities;
using LiteInvoice.Data.Queries;
using Radzen;

namespace LiteInvoice.App.Components.Pages.Entries;

public class WorkEntryGridHelper(DialogService dialog, DapperEntities database) : GridHelper<WorkEntry>(dialog)
{
    private readonly DapperEntities Database = database;

    public int CustomerId { get; set; }
    public int ProjectId { get; set; }

    public decimal HourlyAmount { get; private set; }
    public decimal TotalHours { get; private set; }
    public string AccordionText => $"Hours - {TotalHours} hrs | {HourlyAmount:c2}";

    public override async Task OnDeleteAsync(WorkEntry row) => await Database.WorkEntries.DeleteAsync(row);

    public override async Task OnSaveAsync(WorkEntry row) => await Database.WorkEntries.SaveAsync(row);

    protected override async Task OnRefreshAsync()
    {
        await Task.CompletedTask;
        HourlyAmount = Data.Sum(row => row.Amount);
        TotalHours = Data.Sum(row => row.Hours);
    }

    public override async Task<IEnumerable<WorkEntry>> QueryAsync() =>
        await Database.QueryAsync(new MyPendingWorkEntries() { ProjectId = ProjectId });
}
