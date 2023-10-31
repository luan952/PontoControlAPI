using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PontoControl.Infra.Migrations
{
    public partial class AddIsLoggedInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLogged",
                table: "Users",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLogged",
                table: "Users");
        }
    }
}
