using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace fa18Team22.Migrations
{
    public partial class December2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Procurements_ProcurementID",
                table: "Books");

            //migrationBuilder.DropTable(
                //name: "SortOrderOptions");

            migrationBuilder.DropIndex(
                name: "IX_Books_ProcurementID",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ProcurementID",
                table: "Books");

            migrationBuilder.AlterColumn<bool>(
                name: "ApprovalStatus",
                table: "Reviews",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<int>(
                name: "BookID",
                table: "Procurements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Procurements_BookID",
                table: "Procurements",
                column: "BookID");

            migrationBuilder.AddForeignKey(
                name: "FK_Procurements_Books_BookID",
                table: "Procurements",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "BookID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Procurements_Books_BookID",
                table: "Procurements");

            migrationBuilder.DropIndex(
                name: "IX_Procurements_BookID",
                table: "Procurements");

            migrationBuilder.DropColumn(
                name: "BookID",
                table: "Procurements");

            migrationBuilder.AlterColumn<bool>(
                name: "ApprovalStatus",
                table: "Reviews",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProcurementID",
                table: "Books",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SortOrderOptions",
                columns: table => new
                {
                    SortOrderOptionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SortOption = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SortOrderOptions", x => x.SortOrderOptionID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_ProcurementID",
                table: "Books",
                column: "ProcurementID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Procurements_ProcurementID",
                table: "Books",
                column: "ProcurementID",
                principalTable: "Procurements",
                principalColumn: "ProcurementID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
