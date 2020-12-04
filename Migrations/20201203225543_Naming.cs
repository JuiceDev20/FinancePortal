using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancePortal.Migrations
{
    public partial class Naming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarFile",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarFile",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }
    }
}
