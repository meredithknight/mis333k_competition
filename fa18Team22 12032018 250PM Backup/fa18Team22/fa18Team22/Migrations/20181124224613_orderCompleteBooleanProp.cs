using Microsoft.EntityFrameworkCore.Migrations;

namespace fa18Team22.Migrations
{
    public partial class orderCompleteBooleanProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "Orders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "Orders");
        }
    }
}
