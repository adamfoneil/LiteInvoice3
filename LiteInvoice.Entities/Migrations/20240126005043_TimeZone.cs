using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiteInvoice.Data.Migrations
{
	/// <inheritdoc />
	public partial class TimeZone : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "TimeZoneId",
				table: "AspNetUsers",
				type: "character varying(50)",
				maxLength: 50,
				nullable: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "TimeZoneId",
				table: "AspNetUsers");
		}
	}
}
