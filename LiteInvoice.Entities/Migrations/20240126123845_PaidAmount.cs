using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiteInvoice.Data.Migrations
{
	/// <inheritdoc />
	public partial class PaidAmount : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<decimal>(
				name: "Amount",
				table: "Invoices",
				type: "numeric(6,2)",
				nullable: false,
				oldClrType: typeof(decimal),
				oldType: "numeric");

			migrationBuilder.AddColumn<decimal>(
				name: "PaidAmount",
				table: "Invoices",
				type: "numeric(6,2)",
				nullable: false,
				defaultValue: 0m);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "PaidAmount",
				table: "Invoices");

			migrationBuilder.AlterColumn<decimal>(
				name: "Amount",
				table: "Invoices",
				type: "numeric",
				nullable: false,
				oldClrType: typeof(decimal),
				oldType: "numeric(6,2)");
		}
	}
}
