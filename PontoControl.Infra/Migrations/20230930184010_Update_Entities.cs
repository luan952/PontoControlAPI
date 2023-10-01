using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PontoControl.Infra.Migrations
{
    public partial class Update_Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Markings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstlogin",
                table: "Collaborators",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Markings");

            migrationBuilder.DropColumn(
                name: "IsFirstlogin",
                table: "Collaborators");
        }
    }
}
