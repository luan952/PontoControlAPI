using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PontoControl.Infra.Migrations
{
    public partial class IsFirstLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsLogged",
                table: "Users",
                newName: "IsFirstLogin");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsFirstLogin",
                table: "Users",
                newName: "IsLogged");
        }
    }
}
