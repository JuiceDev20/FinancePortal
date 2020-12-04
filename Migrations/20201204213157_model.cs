using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancePortal.Migrations
{
    public partial class model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "HouseholdInvitation",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "HouseholdInvitation");
        }
    }
}
