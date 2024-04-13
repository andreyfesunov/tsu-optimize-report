using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendBase.Migrations
{
    public partial class ChangeInstituteDbStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_States_StateId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Institutes_Departments_DepartmentId",
                table: "Institutes");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_States_StateId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "StateUser");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_StateId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Institutes_DepartmentId",
                table: "Institutes");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Institutes");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "Departments",
                newName: "InstituteId");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_StateId",
                table: "Departments",
                newName: "IX_Departments_InstituteId");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "States",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "JobId",
                table: "States",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "States",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_States_DepartmentId",
                table: "States",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_States_JobId",
                table: "States",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_States_UserId",
                table: "States",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Institutes_InstituteId",
                table: "Departments",
                column: "InstituteId",
                principalTable: "Institutes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_States_Departments_DepartmentId",
                table: "States",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_States_Jobs_JobId",
                table: "States",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_States_Users_UserId",
                table: "States",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Institutes_InstituteId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_States_Departments_DepartmentId",
                table: "States");

            migrationBuilder.DropForeignKey(
                name: "FK_States_Jobs_JobId",
                table: "States");

            migrationBuilder.DropForeignKey(
                name: "FK_States_Users_UserId",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_States_DepartmentId",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_States_JobId",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_States_UserId",
                table: "States");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "States");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "States");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "States");

            migrationBuilder.RenameColumn(
                name: "InstituteId",
                table: "Departments",
                newName: "StateId");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_InstituteId",
                table: "Departments",
                newName: "IX_Departments_StateId");

            migrationBuilder.AddColumn<Guid>(
                name: "StateId",
                table: "Jobs",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "Institutes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StateUser",
                columns: table => new
                {
                    StatesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateUser", x => new { x.StatesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_StateUser_States_StatesId",
                        column: x => x.StatesId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StateUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_StateId",
                table: "Jobs",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Institutes_DepartmentId",
                table: "Institutes",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StateUser_UsersId",
                table: "StateUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_States_StateId",
                table: "Departments",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Institutes_Departments_DepartmentId",
                table: "Institutes",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_States_StateId",
                table: "Jobs",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id");
        }
    }
}
