using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancePortal.Migrations
{
    public partial class Modeladjust : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FPUserId",
                table: "Transaction",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(35)",
                oldMaxLength: 35,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FPUserId",
                table: "Transaction",
                type: "character varying(35)",
                maxLength: 35,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
