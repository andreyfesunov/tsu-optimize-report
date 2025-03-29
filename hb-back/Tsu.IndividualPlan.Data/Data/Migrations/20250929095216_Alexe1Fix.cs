using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tsu.IndividualPlan.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class Alexe1Fix : Migration
    {
        /// <inheritdoc />
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"UPDATE public.""Users""
                SET ""Firstname"" = LEFT(""Firstname"", LENGTH(""Firstname"") - 1) || 'й'
                WHERE RIGHT(""Firstname"", 1) = '1';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
