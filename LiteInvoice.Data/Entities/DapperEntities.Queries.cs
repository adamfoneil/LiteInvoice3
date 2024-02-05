using Dapper.QX;
using LiteInvoice.Data.Interfaces;
using System.Data;

namespace LiteInvoice.Data.Entities;

public partial class DapperEntities
{
	public async Task<IEnumerable<TResult>> QueryAsync<TResult>(Query<TResult> query)
	{
		using var cn = GetConnection();
		return await QueryAsync(cn, query);
	}

	public async Task<IEnumerable<TResult>> QueryAsync<TResult>(IDbConnection connection, Query<TResult> query)
	{
		if (query is IUserQuery userQuery)
		{
			userQuery.UserId = CurrentUser.UserId;
		}

		var results = await query.ExecuteAsync(connection);

		if (typeof(IHashedResult).IsAssignableFrom(typeof(TResult)))
		{
			foreach (IHashedResult row in results)
			{
				row.HashedId = HashIds.Encode(row.Id);
			}
		}

		return results;
	}
}
