﻿using Dapper;
using Dapper.Entities.PostgreSql;
using LiteInvoice.App.Data;
using LiteInvoice.Data.Entities.Conventions;
using Microsoft.Extensions.Logging;

namespace LiteInvoice.Data.Entities;

public partial class DapperEntities(string connectionString, ILogger<PostgreSqlDatabase> logger) : PostgreSqlDatabase(connectionString, logger, new DefaultSqlBuilder() { CaseConversion = CaseConversionOptions.Exact })
{
	public string CurrentUserName { get; set; } = DefaultUserName;
	public bool IsLoggedIn { get; set; }
	public ApplicationUser CurrentUser { get; set; } = new() { UserName = DefaultUserName, TimeZoneId = DefaultTimeZone };

	public BusinessRepository Businesses => new(this);
	public CustomerRepository Customers => new(this);
	public ProjectRepository Projects => new(this);
	public WorkEntryRepository WorkEntries => new(this);
	public BaseRepository<Invoice> Invoices => new(this);

	public const string DefaultUserName = "system";
	public const string DefaultTimeZone = "America/New_York";

	public async Task LoadCurrentUserAsync()
	{		
		if (CurrentUserName != (CurrentUser?.UserName ?? DefaultUserName))
		{
			Logger.LogInformation("LoadCurrentUserAsync");

			using var cn = GetConnection();
			CurrentUser = await cn.QuerySingleOrDefaultAsync<ApplicationUser>(
				@"SELECT * FROM ""AspNetUsers"" WHERE ""UserName""=@userName",
				new { userName = CurrentUserName }) ?? throw new Exception($"user not found: {CurrentUserName}");

			IsLoggedIn = true;
		}		
	}

	public async Task SetTimeZoneAsync(string timeZoneId)
	{
		using var cn = GetConnection();
		await cn.ExecuteAsync(
			@"UPDATE ""AspNetUsers"" SET ""TimeZoneId""=@timeZoneId WHERE ""UserName""=@userName",
			new { CurrentUser.UserName, timeZoneId });
	}

	public async Task<string> GetTimeZoneAsync()
	{
		using var cn = GetConnection();
		return await cn.QuerySingleOrDefaultAsync<string>(
			@"SELECT ""TimeZoneId"" FROM ""AspNetUsers"" WHERE ""UserName""=@userName",
			new { CurrentUser.UserName }) ?? DefaultTimeZone;
	}

	public static DateTime LocalTime(string? timeZoneId)
	{
		if (timeZoneId == null) return DateTime.UtcNow;

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
