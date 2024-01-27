using Dapper.Entities;
using LiteInvoice.Data.Entities.Conventions;
using LiteInvoice.Data.Queries;
using System.Data;

namespace LiteInvoice.Data.Entities;

public class BusinessRepository(DapperEntities data) : BaseRepository<Business>(data)
{
	protected override async Task AfterSaveAsync(IDbConnection connection, RepositoryAction action, Business entity, IDbTransaction? transaction)
	{
		if (action == RepositoryAction.Insert)
		{
			if (!Database.CurrentUser.CurrentBusinessId.HasValue)
			{
				await Database.QueryAsync(new SetCurrentBusiness() { BusinessId = entity.Id });
				Database.CurrentUser.CurrentBusinessId = entity.Id;
			}
		}
	}
}
