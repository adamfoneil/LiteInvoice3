using HashidsNet;
using LiteInvoice.Data.Entities;
using LiteInvoice.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Data;

namespace Tests;

internal static class Util
{
    internal static IConfiguration Config => new ConfigurationBuilder()
        .AddJsonFile("appsettings.Secret.json")
        .Build();

    internal static string ConnectionString => Config.GetConnectionString("DefaultConnection") ?? throw new Exception("Couldn't find 'DefaultConnection'");

    internal static IDbConnection GetConnection() => new NpgsqlConnection(ConnectionString);

    internal static DapperEntities DapperEntities
	{
		get
        {
			var config = Config.GetSection("HashIds");
			var salt = config.GetValue<string>("Salt");
			int minLength = config.GetValue<int?>("MinLength") ?? 5;
			var hashIds = new Hashids(salt, minLength);

			var connectionString = ConnectionString;
            var logger = new LoggerFactory().CreateLogger<DapperEntities>();
            return new DapperEntities(connectionString, logger, hashIds);
        }
    }
}
