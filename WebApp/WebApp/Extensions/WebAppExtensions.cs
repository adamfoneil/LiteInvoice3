using Dapper.Entities.Interfaces;
using Dapper.QX;
using HashidsNet;
using LiteInvoice.Data.Repositories;
using WebApp.Extensions;

namespace LiteInvoice.Server.Extensions;

internal static class WebAppExtensions
{
	internal static void MapQuery<TQuery, TData>(this IEndpointRouteBuilder routeBuilder, string pattern) where TQuery : Query<TData>, new()
	{
		var query = new TQuery();
		MapQuery<TQuery, TData>(routeBuilder, pattern, query);
	}

	internal static void MapQuery<TQuery, TData>(this IEndpointRouteBuilder routeBuilder, string pattern, TQuery query) where TQuery : Query<TData>
	{
		routeBuilder.MapGet(pattern, async (DapperEntities data, HttpRequest request, HttpContext context, Hashids hashids) =>
		{
			data.CurrentUser = hashids.UserFromRequest(request);
			SetQueryParams<TQuery, TData>(query, request);
			var results = await data.QueryAsync(query);
			return Results.Ok(results);
		});
	}	

	internal static void MapCrud<TEntity>(this IEndpointRouteBuilder routeBuilder, string pattern, Func<DapperEntities, BaseRepository<TEntity>> repository) where TEntity : IEntity<int>
	{
		routeBuilder.MapPost(pattern, async (DapperEntities data, HttpRequest request) =>
		{
			var row = await request.ReadFromJsonAsync<TEntity>() ?? throw new Exception("malformed entity");
			await repository(data).SaveAsync(row);
			return Results.Ok(row);
		});

		routeBuilder.MapPut(pattern, async (DapperEntities data, HttpRequest request) =>
		{
			var row = await request.ReadFromJsonAsync<TEntity>() ?? throw new Exception("malformed entity");
			await repository(data).MergeAsync(row);
			return Results.Ok(row);
		});

		routeBuilder.MapDelete(pattern, async (DapperEntities data, HttpRequest request) =>
		{
			var row = await request.ReadFromJsonAsync<TEntity>() ?? throw new Exception("malformed entity");
			await repository(data).DeleteAsync(row);
			return Results.Ok();
		});
	}

	/// <summary>
	/// attemps to set query parameters from request parameters
	/// </summary>
	private static void SetQueryParams<TQuery, TData>(TQuery query, HttpRequest request) where TQuery : Query<TData>
	{
		var setters = query.GetType()
			.GetProperties().Where(p => p.CanWrite)
			.Join(request.Query, p => p.Name.ToLower(), q => q.Key.ToLower(), (p, k) => new { Property = p, Value = k.Value.First() });

		foreach (var setter in setters)
		{
			var convertValue = ConvertVal(setter.Property.PropertyType, setter.Value!);
			setter.Property.SetValue(query, convertValue);
		}

		static object ConvertVal(Type type, string value)
		{
			if (type.Equals(typeof(int))) return int.Parse(value);
			return value;
		}
	}
}
