using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancePortal.Migrations
{
    public partial class Controller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "CategryItemId",
                table: "Transaction",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Transaction",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "HouseholdId",
                table: "CategoryItem",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "CategoryItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HouseholdCategoryId",
                table: "CategoryItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HouseholdId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_FPUserId",
                table: "Transaction",
                column: "FPUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_HouseholdBankAccountId",
                table: "Transaction",
                column: "HouseholdBankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseholdBankAccount_FPUserId",
                table: "HouseholdBankAccount",
                column: "FPUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryItem_HouseholdCategoryId",
                table: "CategoryItem",
                column: "HouseholdCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryItem_HouseholdId",
                table: "CategoryItem",
                column: "HouseholdId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HouseholdId",
                table: "AspNetUsers",
                column: "HouseholdId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Household_HouseholdId",
                table: "AspNetUsers",
                column: "HouseholdId",
                principalTable: "Household",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryItem_HouseholdCategory_HouseholdCategoryId",
                table: "CategoryItem",
                column: "HouseholdCategoryId",
                principalTable: "HouseholdCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryItem_Household_HouseholdId",
                table: "CategoryItem",
                column: "HouseholdId",
                principalTable: "Household",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HouseholdBankAccount_AspNetUsers_FPUserId",
                table: "HouseholdBankAccount",
                column: "FPUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_AspNetUsers_FPUserId",
                table: "Transaction",
                column: "FPUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_HouseholdBankAccount_HouseholdBankAccountId",
                table: "Transaction",
                column: "HouseholdBankAccountId",
                principalTable: "HouseholdBankAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Household_HouseholdId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryItem_HouseholdCategory_HouseholdCategoryId",
                table: "CategoryItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryItem_Household_HouseholdId",
                table: "CategoryItem");

            migrationBuilder.DropForeignKey(
                name: "FK_HouseholdBankAccount_AspNetUsers_FPUserId",
                table: "HouseholdBankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_AspNetUsers_FPUserId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_HouseholdBankAccount_HouseholdBankAccountId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_FPUserId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_HouseholdBankAccountId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_HouseholdBankAccount_FPUserId",
                table: "HouseholdBankAccount");

            migrationBuilder.DropIndex(
                name: "IX_CategoryItem_HouseholdCategoryId",
                table: "CategoryItem");

            migrationBuilder.DropIndex(
                name: "IX_CategoryItem_HouseholdId",
                table: "CategoryItem");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_HouseholdId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "CategoryItem");

            migrationBuilder.DropColumn(
                name: "HouseholdCategoryId",
                table: "CategoryItem");

            migrationBuilder.DropColumn(
                name: "HouseholdId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "CategryItemId",
                table: "Transaction",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Transaction",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HouseholdId",
                table: "CategoryItem",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }
    }
}
