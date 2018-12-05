using Microsoft.EntityFrameworkCore.Migrations;

namespace fa18Team22.Migrations
{
    public partial class editbookpromoorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AvgRating",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumSpend",
                table: "Promos",
                nullable: false,
                defaultValue: 0m);

        
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinimumSpend",
                table: "Promos");

       
            migrationBuilder.DropColumn(
                name: "AvgRating",
                table: "Books");
        }
    }
}
