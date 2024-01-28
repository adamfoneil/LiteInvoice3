using AO.Radzen.Components.Abstract;
using LiteInvoice.Data.Entities;
using LiteInvoice.Data.Queries;
using Radzen;

namespace LiteInvoice.App.Components.Pages;

public class WorkEntryGridHelper(DialogService dialog, DapperEntities database) : GridHelper<WorkEntry>(dialog)
{
	private readonly DapperEntities Database = database;

	public int CustomerId { get; set; }
	public int ProjectId { get; set; }

	public override async Task OnDeleteAsync(WorkEntry row) => await Database.WorkEntries.DeleteAsync(row);

	public override async Task OnSaveAsync(WorkEntry row) => await Database.WorkEntries.SaveAsync(row);

	public override async Task<IEnumerable<WorkEntry>> QueryAsync() => 
		await Database.QueryAsync(new MyPendingWorkEntries() { ProjectId = ProjectId });	
}
