using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendBase.Migrations
{
    public partial class ChangeReportsStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Users_UserId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_States_Users_UserId",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_States_UserId",
                table: "States");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "States");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Reports",
                newName: "StateUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_UserId",
                table: "Reports",
                newName: "IX_Reports_StateUserId");

            migrationBuilder.CreateTable(
                name: "StateUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    StateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rate = table.Column<double>(type: "double precision", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StateUser_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StateUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StateUser_StateId",
                table: "StateUser",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_StateUser_UserId",
                table: "StateUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_StateUser_StateUserId",
                table: "Reports",
                column: "StateUserId",
                principalTable: "StateUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_StateUser_StateUserId",
                table: "Reports");

            migrationBuilder.DropTable(
                name: "StateUser");

            migrationBuilder.RenameColumn(
                name: "StateUserId",
                table: "Reports",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_StateUserId",
                table: "Reports",
                newName: "IX_Reports_UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "States",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_States_UserId",
                table: "States",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Users_UserId",
                table: "Reports",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_States_Users_UserId",
                table: "States",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
