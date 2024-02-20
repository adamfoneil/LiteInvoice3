using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiteInvoice.Data.Migrations
{
	/// <inheritdoc />
	public partial class SimplifyRateStructure : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "RateSource",
				table: "WorkEntries");

			migrationBuilder.AlterColumn<decimal>(
				name: "HourlyRate",
				table: "Projects",
				type: "numeric(5,2)",
				nullable: false,
				defaultValue: 0m,
				oldClrType: typeof(decimal),
				oldType: "numeric(5,2)",
				oldNullable: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<int>(
				name: "RateSource",
				table: "WorkEntries",
				type: "integer",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.AlterColumn<decimal>(
				name: "HourlyRate",
				table: "Projects",
				type: "numeric(5,2)",
				nullable: true,
				oldClrType: typeof(decimal),
				oldType: "numeric(5,2)");
		}
	}
}
