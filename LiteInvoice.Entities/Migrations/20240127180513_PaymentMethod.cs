using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LiteInvoice.Data.Migrations
{
	/// <inheritdoc />
	public partial class PaymentMethod : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "PaymentMethods",
				columns: table => new
				{
					Id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					BusinessId = table.Column<int>(type: "integer", nullable: false),
					Method = table.Column<int>(type: "integer", nullable: false),
					Data = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
					CreatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
					DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					ModifiedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
					DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_PaymentMethods", x => x.Id);
					table.UniqueConstraint("AK_PaymentMethods_BusinessId_Method", x => new { x.BusinessId, x.Method });
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "PaymentMethods");
		}
	}
}
