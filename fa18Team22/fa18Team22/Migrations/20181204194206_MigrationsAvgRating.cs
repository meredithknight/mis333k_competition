using Microsoft.EntityFrameworkCore.Migrations;

namespace fa18Team22.Migrations
{
    public partial class MigrationsAvgRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvgRating",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "Payment",
                table: "Orders",
                maxLength: 16,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Payment",
                table: "Orders");

            migrationBuilder.AddColumn<decimal>(
                name: "AvgRating",
                table: "Books",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
