using Dapper.Entities;
using LiteInvoice.Entities;
using LiteInvoice.Server;
using LiteInvoice.Server.Queries;
using System.Data;

namespace LiteInvoice.Data.Repositories;

public class BusinessRepository(DapperEntities data) : BaseRepository<Business>(data)
{
	protected override async Task AfterGetAsync(IDbConnection connection, Business entity, IDbTransaction? transaction)
	{
		entity.PaymentMethods = entity.GetPaymentMethods();
		await Task.CompletedTask;
	}

	protected override async Task BeforeSaveAsync(IDbConnection connection, RepositoryAction action, Business entity, IDbTransaction? transaction)
	{
		foreach (var paymentMethod in entity.PaymentMethods)
		{
			paymentMethod.Setter.Invoke(paymentMethod.IsEnabled, paymentMethod.MyLink);
		}

		await base.BeforeSaveAsync(connection, action, entity, transaction);
	}

	protected override async Task AfterSaveAsync(IDbConnection connection, RepositoryAction action, Business entity, IDbTransaction? transaction)
	{
		if (action == RepositoryAction.Insert)
		{
			if (!Database.CurrentUser.CurrentBusinessId.HasValue)
			{
				await Database.QueryAsync(new SetCurrentBusiness() { BusinessId = entity.Id });
				Database.CurrentUser.CurrentBusinessId = entity.Id;
			}
		}
	}
}
