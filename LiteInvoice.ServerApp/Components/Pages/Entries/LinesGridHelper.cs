﻿using AO.Radzen.Components.Abstract;
using LiteInvoice.Entities;
using LiteInvoice.Server;
using LiteInvoice.Server.Queries;
using Radzen;

namespace LiteInvoice.ServerApp.Components.Pages.Entries
{
	public class LineEntryGridHelper(DialogService dialogs, DapperEntities data) : GridHelper<LineEntry>(dialogs)
    {
        private readonly DapperEntities Database = data;

        public int ProjectId { get; set; }

        public decimal Amount { get; private set; }

		protected override async Task OnRefreshAsync()
		{
            await Task.CompletedTask;
            Amount = Data.Sum(row => row.Amount);
		}

		public override async Task OnDeleteAsync(LineEntry row) => await Database.LineEntries.DeleteAsync(row);

        public override async Task OnSaveAsync(LineEntry data) => await Database.LineEntries.SaveAsync(data);

        public override async Task<IEnumerable<LineEntry>> QueryAsync() =>
            await Database.QueryAsync(new MyPendingLineEntries() { ProjectId = ProjectId });
    }
}
