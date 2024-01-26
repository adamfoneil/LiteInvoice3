using Dapper.Entities;
using Dapper.Entities.Interfaces;
using LiteInvoice.Data.Entities.Interfaces;
using System.Data;

namespace LiteInvoice.Data.Entities.Conventions;

public class BaseRepository<TEntity>(DapperEntities database) : Repository<DapperEntities, TEntity, int>(database) where TEntity : IEntity<int>
{
	protected override async Task BeforeSaveAsync(IDbConnection connection, RepositoryAction action, TEntity entity, IDbTransaction? transaction)
	{
		await Task.CompletedTask;

		if (entity is IUserTable userTable && action == RepositoryAction.Insert)
		{
			userTable.UserId = Database.UserId;
		}

		if (entity is BaseTable baseTable)
		{
			var localTime = LocalTime(Database.TimeZoneId);
			switch (action)
			{
				case RepositoryAction.Insert:
					baseTable.CreatedBy = Database.UserName;
					baseTable.DateCreated = localTime;
					break;

				case RepositoryAction.Update:
					baseTable.ModifiedBy = Database.UserName;
					baseTable.DateModified = localTime;
					break;
			}
		}

		static DateTime LocalTime(string timeZoneId)
		{
			try
			{
				var tz = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
				return TimeZoneInfo.ConvertTime(DateTime.UtcNow, tz);
			}
			catch
			{
				return DateTime.UtcNow;
			}
		}
	}
}
