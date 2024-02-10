using LiteInvoice.Data.Entities;
using Microsoft.AspNetCore.Components;

namespace LiteInvoice.ServerApp;

public class DataComponent : ComponentBase
{	
	[Inject]
	public DapperEntities Data { get; set; } = default!;

	protected override async Task OnInitializedAsync()
	{
		if (Data.IsLoggedIn) await Data.LoadCurrentUserAsync();
	}
}
