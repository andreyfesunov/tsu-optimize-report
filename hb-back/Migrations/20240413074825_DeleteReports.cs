using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendBase.Migrations
{
    public partial class DeleteReports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Reports_ReportId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "StateUser");

            migrationBuilder.DropIndex(
                name: "IX_Events_ReportId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "Events");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReportId",
                table: "Events",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StateUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StateId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: false),
                    StateUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_StateUser_StateUserId",
                        column: x => x.StateUserId,
                        principalTable: "StateUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_ReportId",
                table: "Events",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_FileId",
                table: "Reports",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_StateUserId",
                table: "Reports",
                column: "StateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StateUser_StateId",
                table: "StateUser",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_StateUser_UserId",
                table: "StateUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Reports_ReportId",
                table: "Events",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id");
        }
    }
}
