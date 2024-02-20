using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LiteInvoice.Data.Migrations
{
	/// <inheritdoc />
	public partial class CustomerTable2 : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<int>(
				name: "RateSource",
				table: "WorkEntries",
				type: "integer",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.CreateTable(
				name: "Customers",
				columns: table => new
				{
					Id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					BusinessId = table.Column<int>(type: "integer", nullable: false),
					Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
					Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
					PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
					MailingAddress = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
					City = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
					State = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
					PostalCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
					Country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
					HourlyRate = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
					CreatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
					DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					ModifiedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
					DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Customers", x => x.Id);
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Customers");

			migrationBuilder.DropColumn(
				name: "RateSource",
				table: "WorkEntries");
		}
	}
}
