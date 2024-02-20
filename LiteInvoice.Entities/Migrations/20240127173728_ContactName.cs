using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiteInvoice.Data.Migrations
{
	/// <inheritdoc />
	public partial class ContactName : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "ContactName",
				table: "Customers",
				type: "character varying(50)",
				maxLength: 50,
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "ContactName",
				table: "Businesses",
				type: "character varying(50)",
				maxLength: 50,
				nullable: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "ContactName",
				table: "Customers");

			migrationBuilder.DropColumn(
				name: "ContactName",
				table: "Businesses");
		}
	}
}
