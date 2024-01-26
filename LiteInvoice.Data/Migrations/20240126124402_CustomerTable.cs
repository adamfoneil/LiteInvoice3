using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiteInvoice.Data.Migrations
{
    /// <inheritdoc />
    public partial class CustomerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "Projects",
                newName: "CustomerId");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Businesses",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Businesses");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Projects",
                newName: "BusinessId");
        }
    }
}
