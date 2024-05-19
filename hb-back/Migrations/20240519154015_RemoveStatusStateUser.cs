using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendBase.Migrations
{
    public partial class RemoveStatusStateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "StatesUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "StatesUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
