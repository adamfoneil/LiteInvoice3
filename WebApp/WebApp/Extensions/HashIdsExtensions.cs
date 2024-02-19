using HashidsNet;
using LiteInvoice.Entities;

namespace WebApp.Extensions;

internal static class HashIdsExtensions
{
	internal static ApplicationUser UserFromRequest(this Hashids hashids, HttpRequest request)
	{
		var userId = hashids.DecodeSingle(request.Headers.Authorization);
		return new() { UserId = userId };
	}
}
