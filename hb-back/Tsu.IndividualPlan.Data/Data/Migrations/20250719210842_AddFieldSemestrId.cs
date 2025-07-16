using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tsu.IndividualPlan.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldSemestrId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SemestrId",
                table: "Records",
                type: "smallint",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<string>(
                name: "SemestrId",
                table: "Events",
                type: "smallint",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SemestrId",
                table: "Records");
            migrationBuilder.DropColumn(
                name: "SemestrId",
                table: "Events");
        }
    }
}
