using Microsoft.EntityFrameworkCore.Migrations;

namespace fa18Team22.Migrations
{
    public partial class editreview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_AppUserId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_AppUserId1",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "AppUserId1",
                table: "Reviews",
                newName: "AuthorId");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Reviews",
                newName: "ApproverId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_AppUserId1",
                table: "Reviews",
                newName: "IX_Reviews_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_AppUserId",
                table: "Reviews",
                newName: "IX_Reviews_ApproverId");

            migrationBuilder.AddColumn<bool>(
                name: "ApprovalStatus",
                table: "Reviews",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Procurements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Procurements_EmployeeId",
                table: "Procurements",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Procurements_AspNetUsers_EmployeeId",
                table: "Procurements",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_ApproverId",
                table: "Reviews",
                column: "ApproverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_AuthorId",
                table: "Reviews",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Procurements_AspNetUsers_EmployeeId",
                table: "Procurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_ApproverId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_AuthorId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Procurements_EmployeeId",
                table: "Procurements");

            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Procurements");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Reviews",
                newName: "AppUserId1");

            migrationBuilder.RenameColumn(
                name: "ApproverId",
                table: "Reviews",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_AuthorId",
                table: "Reviews",
                newName: "IX_Reviews_AppUserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ApproverId",
                table: "Reviews",
                newName: "IX_Reviews_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_AppUserId",
                table: "Reviews",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_AppUserId1",
                table: "Reviews",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
