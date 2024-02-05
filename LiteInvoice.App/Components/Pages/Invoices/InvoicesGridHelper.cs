using AO.Radzen.Components.Abstract;
using LiteInvoice.Data.Entities;
using LiteInvoice.Data.Queries;
using Radzen;

namespace LiteInvoice.App.Components.Pages.Invoices;

public class InvoicesGridHelper(DapperEntities database, DialogService dialogs) : GridHelper<Invoice>(dialogs)
{
	private readonly DapperEntities Database = database;

	public override async Task OnDeleteAsync(Invoice row) => await Database.DeleteInvoiceAsync(row.Id);

	public override async Task OnSaveAsync(Invoice row) => await Database.Invoices.SaveAsync(row);
	
	public override async Task<IEnumerable<Invoice>> QueryAsync() => await Database.QueryAsync(new MyInvoices());	
}
