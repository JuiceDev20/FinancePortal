using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancePortal.Migrations
{
    public partial class ViewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TargetAmount",
                table: "CategoryItem",
                type: "decimal(10, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ActualAmount",
                table: "CategoryItem",
                type: "decimal(10, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6, 2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TargetAmount",
                table: "CategoryItem",
                type: "decimal(6, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ActualAmount",
                table: "CategoryItem",
                type: "decimal(6, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10, 2)");
        }
    }
}
