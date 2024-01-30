using LiteInvoice.Data.Entities.Conventions;

namespace LiteInvoice.Data.Entities;

public class InvoiceRepository(DapperEntities dapperEntities) : BaseRepository<Invoice>(dapperEntities)
{
	public async Task CreateAsync(int projectId)
	{
		throw new NotImplementedException();
	}
}
