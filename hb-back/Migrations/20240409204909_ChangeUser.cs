using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendBase.Migrations
{
    public partial class ChangeUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nickname",
                table: "Users",
                newName: "Lastname");

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Users",
                newName: "Nickname");
        }
    }
}
