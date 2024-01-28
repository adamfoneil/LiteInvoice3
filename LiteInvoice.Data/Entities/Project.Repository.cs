using Dapper.Entities;
using LiteInvoice.Data.Entities.Conventions;
using System.Data;

namespace LiteInvoice.Data.Entities;

public class ProjectRepository(DapperEntities database) : BaseRepository<Project>(database)
{
	protected override async Task BeforeSaveAsync(IDbConnection connection, RepositoryAction action, Project entity, IDbTransaction? transaction)
	{
		if (action == RepositoryAction.Insert && entity.HourlyRate == 0)
		{
			var project = await Database.Customers.GetAsync(connection, entity.CustomerId) ?? throw new Exception($"Customer Id {entity.CustomerId} not found");
			entity.HourlyRate = project.HourlyRate;
		}

		await base.BeforeSaveAsync(connection, action, entity, transaction);
	}
}
