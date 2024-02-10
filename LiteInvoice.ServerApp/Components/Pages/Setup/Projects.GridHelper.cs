using AO.Radzen.Components.Abstract;
using LiteInvoice.Data.Entities;
using LiteInvoice.Data.Queries;
using Radzen;

namespace LiteInvoice.ServerApp.Components.Pages.Setup;

public class ProjectsGridHelper(DialogService dialog, DapperEntities data) : GridHelper<Project>(dialog)
{
    private readonly DapperEntities Database = data;

    public int CustomerId { get; set; }

    public override async Task OnDeleteAsync(Project row) => await Database.Projects.DeleteAsync(row);
    
    public override async Task OnSaveAsync(Project data) => await Database.Projects.SaveAsync(data);

    public override async Task<IEnumerable<Project>> QueryAsync() => await Database.QueryAsync(new MyProjects() {  CustomerId = CustomerId });
    
}
