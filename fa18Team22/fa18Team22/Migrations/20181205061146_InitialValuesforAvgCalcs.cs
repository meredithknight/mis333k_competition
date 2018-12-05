using Microsoft.EntityFrameworkCore.Migrations;

namespace fa18Team22.Migrations
{
    public partial class InitialValuesforAvgCalcs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "InitialCost",
                table: "Books",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "InitialInventory",
                table: "Books",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "InitialSalesPrice",
                table: "Books",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialCost",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "InitialInventory",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "InitialSalesPrice",
                table: "Books");
        }
    }
}
