using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PontoControl.Infra.Migrations
{
    public partial class RemoveIsFirstLoginCollaborator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFirstlogin",
                table: "Collaborators");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFirstlogin",
                table: "Collaborators",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
