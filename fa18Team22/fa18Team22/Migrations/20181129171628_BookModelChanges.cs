using Microsoft.EntityFrameworkCore.Migrations;

namespace fa18Team22.Migrations
{
    public partial class BookModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AvgBookCost",
                table: "Books",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvgBookCost",
                table: "Books");
        }
    }
}
