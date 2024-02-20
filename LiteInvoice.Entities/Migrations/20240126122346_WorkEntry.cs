using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LiteInvoice.Data.Migrations
{
	/// <inheritdoc />
	public partial class WorkEntry : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropPrimaryKey(
				name: "PK_Business",
				table: "Business");

			migrationBuilder.RenameTable(
				name: "Business",
				newName: "Businesses");

			migrationBuilder.AddPrimaryKey(
				name: "PK_Businesses",
				table: "Businesses",
				column: "Id");

			migrationBuilder.CreateTable(
				name: "Invoices",
				columns: table => new
				{
					Id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					BusinessId = table.Column<int>(type: "integer", nullable: false),
					Number = table.Column<int>(type: "integer", nullable: false),
					Amount = table.Column<decimal>(type: "numeric", nullable: false),
					CreatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
					DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					ModifiedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
					DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Invoices", x => x.Id);
					table.UniqueConstraint("AK_Invoices_BusinessId_Number", x => new { x.BusinessId, x.Number });
				});

			migrationBuilder.CreateTable(
				name: "Projects",
				columns: table => new
				{
					Id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					BusinessId = table.Column<int>(type: "integer", nullable: false),
					Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
					Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
					HourlyRate = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
					IsActive = table.Column<bool>(type: "boolean", nullable: false),
					CreatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
					DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					ModifiedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
					DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Projects", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "WorkEntries",
				columns: table => new
				{
					Id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					ProjectId = table.Column<int>(type: "integer", nullable: false),
					Date = table.Column<DateOnly>(type: "date", nullable: false),
					Comments = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
					Hours = table.Column<decimal>(type: "numeric(4,2)", nullable: false),
					HourlyRate = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
					InvoiceId = table.Column<int>(type: "integer", nullable: true),
					CreatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
					DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					ModifiedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
					DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_WorkEntries", x => x.Id);
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Invoices");

			migrationBuilder.DropTable(
				name: "Projects");

			migrationBuilder.DropTable(
				name: "WorkEntries");

			migrationBuilder.DropPrimaryKey(
				name: "PK_Businesses",
				table: "Businesses");

			migrationBuilder.RenameTable(
				name: "Businesses",
				newName: "Business");

			migrationBuilder.AddPrimaryKey(
				name: "PK_Business",
				table: "Business",
				column: "Id");
		}
	}
}
