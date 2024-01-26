using LiteInvoice.Data.Entities;
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
            var connectionString = ConnectionString;
            var logger = new LoggerFactory().CreateLogger<DapperEntities>();
            return new DapperEntities(connectionString, logger);
        }
    }
}
