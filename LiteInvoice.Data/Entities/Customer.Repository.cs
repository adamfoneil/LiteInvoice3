using Dapper.Entities;
using LiteInvoice.Data.Entities.Conventions;
using System.Data;

namespace LiteInvoice.Data.Entities;

public class CustomerRepository(DapperEntities database) : BaseRepository<Customer>(database)
{
	protected override async Task BeforeSaveAsync(IDbConnection connection, RepositoryAction action, Customer entity, IDbTransaction? transaction)
	{
		if (action == RepositoryAction.Insert)
		{
			entity.BusinessId = Database.CurrentUser.CurrentBusinessId ?? 0;
		}

		await base.BeforeSaveAsync(connection, action, entity, transaction);
	}
}
