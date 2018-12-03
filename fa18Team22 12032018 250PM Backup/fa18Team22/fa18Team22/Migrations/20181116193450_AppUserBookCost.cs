using Microsoft.EntityFrameworkCore.Migrations;

namespace fa18Team22.Migrations
{
    public partial class AppUserBookCost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_CustomerEmailAddress",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserEmailAddress",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserEmailAddress1",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.RenameColumn(
                name: "UserEmailAddress1",
                table: "Reviews",
                newName: "AppUserEmailAddress1");

            migrationBuilder.RenameColumn(
                name: "UserEmailAddress",
                table: "Reviews",
                newName: "AppUserEmailAddress");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_UserEmailAddress1",
                table: "Reviews",
                newName: "IX_Reviews_AppUserEmailAddress1");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_UserEmailAddress",
                table: "Reviews",
                newName: "IX_Reviews_AppUserEmailAddress");

            migrationBuilder.AddColumn<decimal>(
                name: "BookCost",
                table: "Books",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    EmailAddress = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Zip = table.Column<string>(maxLength: 5, nullable: false),
                    Password = table.Column<string>(maxLength: 15, nullable: false),
                    CreditCard1 = table.Column<string>(nullable: true),
                    CreditCard2 = table.Column<string>(nullable: true),
                    CreditCard3 = table.Column<string>(nullable: true),
                    UserStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.EmailAddress);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AppUsers_CustomerEmailAddress",
                table: "Orders",
                column: "CustomerEmailAddress",
                principalTable: "AppUsers",
                principalColumn: "EmailAddress",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AppUsers_AppUserEmailAddress",
                table: "Reviews",
                column: "AppUserEmailAddress",
                principalTable: "AppUsers",
                principalColumn: "EmailAddress",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AppUsers_AppUserEmailAddress1",
                table: "Reviews",
                column: "AppUserEmailAddress1",
                principalTable: "AppUsers",
                principalColumn: "EmailAddress",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AppUsers_CustomerEmailAddress",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AppUsers_AppUserEmailAddress",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AppUsers_AppUserEmailAddress1",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropColumn(
                name: "BookCost",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "AppUserEmailAddress1",
                table: "Reviews",
                newName: "UserEmailAddress1");

            migrationBuilder.RenameColumn(
                name: "AppUserEmailAddress",
                table: "Reviews",
                newName: "UserEmailAddress");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_AppUserEmailAddress1",
                table: "Reviews",
                newName: "IX_Reviews_UserEmailAddress1");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_AppUserEmailAddress",
                table: "Reviews",
                newName: "IX_Reviews_UserEmailAddress");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    EmailAddress = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    CreditCard1 = table.Column<string>(nullable: true),
                    CreditCard2 = table.Column<string>(nullable: true),
                    CreditCard3 = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(maxLength: 15, nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    UserStatus = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.EmailAddress);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_CustomerEmailAddress",
                table: "Orders",
                column: "CustomerEmailAddress",
                principalTable: "Users",
                principalColumn: "EmailAddress",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserEmailAddress",
                table: "Reviews",
                column: "UserEmailAddress",
                principalTable: "Users",
                principalColumn: "EmailAddress",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserEmailAddress1",
                table: "Reviews",
                column: "UserEmailAddress1",
                principalTable: "Users",
                principalColumn: "EmailAddress",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
