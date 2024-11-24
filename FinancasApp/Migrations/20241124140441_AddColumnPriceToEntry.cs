using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancasApp.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnPriceToEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "entries",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "entries");
        }
    }
}
