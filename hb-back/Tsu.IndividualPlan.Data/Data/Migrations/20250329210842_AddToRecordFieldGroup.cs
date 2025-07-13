using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tsu.IndividualPlan.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddToRecordFieldGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupString",
                table: "Records",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupString",
                table: "Records");
        }
    }
}
