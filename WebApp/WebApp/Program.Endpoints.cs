using LiteInvoice.Entities;
using LiteInvoice.Server;
using LiteInvoice.Server.Extensions;
using LiteInvoice.Server.Queries;

namespace WebApp;

internal static partial class Program
{
	/*
	public static void MapTokenEndpoint(this IEndpointRouteBuilder routeBuilder)
	{
		routeBuilder.MapPost("/Token", async (DapperEntities data, TokenRequest request) =>
		{
			var user = await data.Users.GetByEmailAsync(request.Email);
			if (user is null || !user.Password.Verify(request.Password))
			{
				return Results.Unauthorized();
			}

			var token = user.GenerateToken();
			return Results.Ok(token);
		});
	}
	*/

	public static void MapQueries(this IEndpointRouteBuilder routeBuilder, params string[] policyNames)
	{
		var group = routeBuilder.MapGroup("/api/Queries").RequireAuthorization(policyNames);
		group.MapQuery<MyBusinesses, Business>("/MyBusinesses");
		group.MapQuery<MyCustomers, Customer>("/MyCustomers");
		group.MapQuery<MyInvoices, Invoice>("/MyInvoices");
		group.MapQuery<MyPendingLineEntries, LineEntry>("/MyPendingLineEntries");
		group.MapQuery<MyPendingWorkEntries, WorkEntry>("/MyPendingWorkEntries");
		group.MapQuery<MyProjects, Project>("/MyProjects");
	}

	public static void MapCrudOperations(this IEndpointRouteBuilder routeBuilder, params string[] policyNames)
	{
		var group = routeBuilder.MapGroup("/api/Entities").RequireAuthorization(policyNames);
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
