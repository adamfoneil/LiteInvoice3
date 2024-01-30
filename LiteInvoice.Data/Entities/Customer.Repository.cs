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
			if (entity.BusinessId == 0)
			{
				entity.BusinessId = Database.CurrentUser.CurrentBusinessId ?? 0;
			}

			if (entity.HourlyRate == 0)
			{
				var project = await Database.Businesses.GetAsync(connection, entity.BusinessId) ?? throw new Exception($"Business Id {entity.BusinessId} not found");
				entity.HourlyRate = project.HourlyRate;
			}
		}

		await base.BeforeSaveAsync(connection, action, entity, transaction);
	}
}
