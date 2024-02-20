using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiteInvoice.Data.Migrations
{
	/// <inheritdoc />
	public partial class ForeignKeys : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateIndex(
				name: "IX_WorkEntries_ProjectId",
				table: "WorkEntries",
				column: "ProjectId");

			migrationBuilder.CreateIndex(
				name: "IX_Customers_BusinessId",
				table: "Customers",
				column: "BusinessId");

			migrationBuilder.AddForeignKey(
				name: "FK_Customers_Businesses_BusinessId",
				table: "Customers",
				column: "BusinessId",
				principalTable: "Businesses",
				principalColumn: "Id",
				onDelete: ReferentialAction.NoAction);

			migrationBuilder.AddForeignKey(
				name: "FK_Projects_Customers_CustomerId",
				table: "Projects",
				column: "CustomerId",
				principalTable: "Customers",
				principalColumn: "Id",
				onDelete: ReferentialAction.NoAction);

			migrationBuilder.AddForeignKey(
				name: "FK_WorkEntries_Projects_ProjectId",
				table: "WorkEntries",
				column: "ProjectId",
				principalTable: "Projects",
				principalColumn: "Id",
				onDelete: ReferentialAction.NoAction);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Customers_Businesses_BusinessId",
				table: "Customers");

			migrationBuilder.DropForeignKey(
				name: "FK_WorkEntries_Projects_ProjectId",
				table: "WorkEntries");

			migrationBuilder.DropForeignKey(
				name: "FK_Projects_Customers_CustomerId",
				table: "Projects");

			migrationBuilder.DropIndex(
				name: "IX_WorkEntries_ProjectId",
				table: "WorkEntries");

			migrationBuilder.DropIndex(
				name: "IX_Customers_BusinessId",
				table: "Customers");
		}
	}
}
