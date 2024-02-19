using LiteInvoice.Entities;
using LiteInvoice.Server;
using LiteInvoice.Server.Extensions;
using LiteInvoice.Server.Queries;

namespace WebApp;

internal static partial class Program
{
    public static void MapQueries(this IEndpointRouteBuilder routeBuilder)
    {
        var group = routeBuilder.MapGroup("/Queries");
        group.MapQuery<MyBusinesses, Business>("/MyBusinesses");
        group.MapQuery<MyCustomers, Customer>("/MyCustomers");
        group.MapQuery<MyInvoices, Invoice>("/MyInvoices");
        group.MapQuery<MyPendingLineEntries, LineEntry>("/MyPendingLineEntries");
        group.MapQuery<MyPendingWorkEntries, WorkEntry>("/MyPendingWorkEntries");
        group.MapQuery<MyProjects, Project>("/MyProjects");
    }

    public static void MapCrudOperations(this IEndpointRouteBuilder routeBuilder)
    {
        var group = routeBuilder.MapGroup("/Entities").RequireAuthorization();
        group.MapCrud("/Customers", (data) => data.Customers);
        group.MapCrud("/Projects", (data) => data.Projects);
        group.MapCrud("/WorkEntries", (data) => data.WorkEntries);
        group.MapCrud("/LineEntries", (data) => data.LineEntries);
        group.MapCrud("/Invoices", (data) => data.Invoices);
        
        routeBuilder.MapPost("/Invoice/{projectId:int}", async (DapperEntities data, int projectId) =>
        {
            var result = await data.Invoices.CreateAsync(projectId);
            return Results.Ok(result);
        });
    }
}
