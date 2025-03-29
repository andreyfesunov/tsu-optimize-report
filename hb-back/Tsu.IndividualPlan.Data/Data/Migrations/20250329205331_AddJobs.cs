using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tsu.IndividualPlan.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddJobs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE EXTENSION IF NOT EXISTS ""uuid-ossp"";
                INSERT INTO public.""Jobs"" (""Id"", ""Name"")
                VALUES
                  (uuid_generate_v4(), 'Преподаватель'),
                  (uuid_generate_v4(), 'Старший преподаватель'),
                  (uuid_generate_v4(), 'Ассистент'),
                  (uuid_generate_v4(), 'Профессор'),
                  (uuid_generate_v4(), 'Заведующий кафедрой'),
                  (uuid_generate_v4(), 'Директор института');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
