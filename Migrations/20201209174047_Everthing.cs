using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancePortal.Migrations
{
    public partial class Everthing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryItemId",
                table: "Transaction",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CategoryItemId",
                table: "Transaction",
                column: "CategoryItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_CategoryItem_CategoryItemId",
                table: "Transaction",
                column: "CategoryItemId",
                principalTable: "CategoryItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_CategoryItem_CategoryItemId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_CategoryItemId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CategoryItemId",
                table: "Transaction");
        }
    }
}
