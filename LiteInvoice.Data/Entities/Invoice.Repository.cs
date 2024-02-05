using HashidsNet;
using LiteInvoice.Data.Entities.Conventions;

namespace LiteInvoice.Data.Entities;

public class InvoiceRepository(DapperEntities dapperEntities, Hashids hashIds) : BaseRepository<Invoice>(dapperEntities)
{
	private readonly Hashids HashIds = hashIds;

	public async Task<Invoice> GetAsync(string hashId)
	{
		var invoiceId = HashIds.DecodeSingle(hashId);
		return await GetAsync(invoiceId);
	}

	public async Task CreateAsync(int projectId)
	{
		throw new NotImplementedException();
	}
}
