using Dapper.Entities;
using LiteInvoice.Data.Entities.Conventions;
using System.Data;

namespace LiteInvoice.Data.Entities;

public class WorkEntryRepository(DapperEntities database) : BaseRepository<WorkEntry>(database)
{
	protected override async Task BeforeSaveAsync(IDbConnection connection, RepositoryAction action, WorkEntry entity, IDbTransaction? transaction)
	{
		if (action == RepositoryAction.Insert && entity.HourlyRate == 0)
		{
			var project = await Database.Projects.GetAsync(connection, entity.ProjectId) ?? throw new Exception($"Project Id {entity.ProjectId} not found");
			entity.HourlyRate = project.HourlyRate;
		}

		await base.BeforeSaveAsync(connection, action, entity, transaction);
	}
}
