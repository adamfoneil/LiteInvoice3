using LiteInvoice.Data.Entities;

namespace Tests
{
	[TestClass]
	public class DapperEntityTests
	{
		[TestMethod]
		public async Task SetTimeZone()
		{
			var db = Util.DapperEntities;
			db.CurrentUser = new() { UserName = "nobody" };
			await db.SetTimeZoneAsync("anything");
		}

		/// <summary>
		/// checks the inline SQL only, doesn't verify a specific output
		/// </summary>        
		[TestMethod]
		public async Task CreateInvoiceInlineSql()
		{
			var db = Util.DapperEntities;
			db.CurrentUser = new() { UserName = "nobody", TimeZoneId = "America/New_York" };
			await db.CreateInvoiceAsync(-100);
		}

		[TestMethod]
		public async Task CreateInvoiceActual()
		{
			var db = Util.DapperEntities;
			db.CurrentUserName = "adamosoftware@gmail.com";
			await db.LoadCurrentUserAsync();

			var business = await db.Businesses.SaveAsync(new Business()
			{
				DisplayName = "test biz",
				NextInvoiceNumber = 1233,
				HourlyRate = 50
			});

			var customer = await db.Customers.SaveAsync(new Customer()
			{
				BusinessId = business.Id,
				Name = "sample customer"
			});

			var project = await db.Projects.SaveAsync(new Project()
			{
				CustomerId = customer.Id,
				Name = "sample project"
			});

			var we = await db.WorkEntries.SaveAsync(new WorkEntry()
			{
				ProjectId = project.Id,
				Date = DateTime.Today,
				Comments = "whatever",
				Hours = 3
			});

			var le = await db.LineEntries.SaveAsync(new LineEntry()
			{
				ProjectId = project.Id,
				Description = "whatever",
				Amount = 10
			});

			var invoiceId = await db.CreateInvoiceAsync(project.Id);

			var invoice = await db.Invoices.GetAsync(invoiceId);
			Assert.IsTrue(invoice.Number == 1233);
			Assert.IsTrue(invoice.Amount == 160m);

			var biz = await db.Businesses.GetAsync(business.Id);
			Assert.IsTrue(biz.NextInvoiceNumber == 1234);

			//todo: delete test data
		}
	}
}