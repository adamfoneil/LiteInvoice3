using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiteInvoice.Data.Migrations
{
	/// <inheritdoc />
	public partial class PayPalMe : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "PayPalMeLink",
				table: "Businesses",
				type: "character varying(100)",
				maxLength: 100,
				nullable: true);

			migrationBuilder.AddColumn<bool>(
				name: "UsePayPalMe",
				table: "Businesses",
				type: "boolean",
				nullable: false,
				defaultValue: false);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "PayPalMeLink",
				table: "Businesses");

			migrationBuilder.DropColumn(
				name: "UsePayPalMe",
				table: "Businesses");
		}
	}
}
