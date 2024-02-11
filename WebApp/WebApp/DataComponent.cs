using LiteInvoice.Data.Entities;
using Microsoft.AspNetCore.Components;

namespace WebApp;

public class DataComponent : ComponentBase
{	
	[Inject]
	public DapperEntities Data { get; set; } = default!;

	protected override async Task OnInitializedAsync()
	{
		if (Data.IsLoggedIn) await Data.LoadCurrentUserAsync();
	}
}
