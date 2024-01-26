using LiteInvoice.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Tests;

internal static class Util
{
    internal static IConfiguration Config => new ConfigurationBuilder()
        .AddJsonFile("appsettings.Secret.json")
        .Build();

    internal static DapperEntities DapperEntities
    {
        get
        {
            var connectionString = Config.GetConnectionString("DefaultConnection") ?? throw new Exception("Couldn't find 'DefaultConnection'");
            var logger = new LoggerFactory().CreateLogger<DapperEntities>();
            return new DapperEntities(connectionString, logger);
        }
    }
}
