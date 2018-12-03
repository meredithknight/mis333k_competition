using Microsoft.EntityFrameworkCore.Migrations;

namespace fa18Team22.Migrations
{
    public partial class editnewreview1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RejecterId",
                table: "Reviews",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RejecterId",
                table: "Reviews",
                column: "RejecterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_RejecterId",
                table: "Reviews",
                column: "RejecterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_RejecterId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_RejecterId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "RejecterId",
                table: "Reviews");
        }
    }
}
