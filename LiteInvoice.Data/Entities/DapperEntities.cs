using Dapper;
using Dapper.Entities.PostgreSql;
using Microsoft.Extensions.Logging;

namespace LiteInvoice.Data.Entities;

public class DapperEntities(string connectionString, ILogger<PostgreSqlDatabase> logger) : PostgreSqlDatabase(connectionString, logger, new DefaultSqlBuilder() { CaseConversion = CaseConversionOptions.Exact })
{
	public string UserName { get; set; } = "system";
	public string TimeZoneId { get; set; } = "Eastern Standard Time";
	public int UserId { get; set; }

	public async Task SetTimeZoneAsync(string timeZoneId)
	{
		using var cn = GetConnection();
		await cn.ExecuteAsync(
			@"UPDATE ""AspNetUsers"" SET ""TimeZoneId""=@timeZoneId WHERE ""UserName""=@userName",
			new { UserName, timeZoneId });
	}
}
