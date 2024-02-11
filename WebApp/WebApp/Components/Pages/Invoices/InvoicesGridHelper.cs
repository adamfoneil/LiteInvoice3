using AO.Radzen.Components.Abstract;
using LiteInvoice.Entities;
using LiteInvoice.Server;
using LiteInvoice.Server.Queries;
using Radzen;

namespace WebApp.Components.Pages.Invoices;

public class InvoicesGridHelper(DapperEntities database, DialogService dialogs) : GridHelper<Invoice>(dialogs)
{
	private readonly DapperEntities Database = database;

	public override async Task OnDeleteAsync(Invoice row) => await Database.Invoices.DeleteAsync(row.Id);

	public override async Task OnSaveAsync(Invoice row) => await Database.Invoices.SaveAsync(row);
	
	public override async Task<IEnumerable<Invoice>> QueryAsync() => await Database.QueryAsync(new MyInvoices());
}
