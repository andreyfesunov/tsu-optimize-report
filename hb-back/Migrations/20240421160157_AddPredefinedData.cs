using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendBase.Migrations
{
    public partial class AddPredefinedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Column",
                table: "Activities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "Id", "Column", "Name" },
                values: new object[,]
                {
                    { new Guid("1189dfd3-7275-446e-8be2-5715f979744d"), "S", "Практические (семинарские) занятия" },
                    { new Guid("127c7662-11ee-4887-8042-0d8b5c1b2fdb"), "Y", "Предэкзаменационные консультации" },
                    { new Guid("169316c8-b05b-4f93-915b-51a3cdfa78e0"), "AP", "Участие в работе АК" },
                    { new Guid("3f9f3b78-ea23-4f6a-a9d7-d144bec23db8"), "AO", "ГЭК по защитам ВКР" },
                    { new Guid("4bf73e1c-08f0-4293-b542-ff59ad344e9a"), "AK", "Экзамен" },
                    { new Guid("4d45ee78-9b1d-49c8-81fa-e63be9331926"), "X", "Индивидуальные занятия" },
                    { new Guid("56d19396-7fd1-4bb4-b0cd-8f61be9874ef"), "AB", "Прием КР,  РГР, ГР, ИБ, письменных заданий" },
                    { new Guid("762f1ae4-356d-438b-bbb5-6ee079247731"), "AE", "Зачет" },
                    { new Guid("8440bb26-b652-47c0-8844-e0d67216f2aa"), "U", "Лабораторные работы, клинические практические занятия" },
                    { new Guid("84b0e2aa-4f8e-4f0f-98e8-d1264a414db8"), "AL", "Государственный экзамен" },
                    { new Guid("8d6d6cd5-f0e0-4cf9-8e6b-8683a6938f9a"), "AD", "Руководство практикой, НИР, НИД" },
                    { new Guid("8d8b086f-e4a1-4b77-84aa-a1b69941391d"), "AM", "Руководство ВКР" },
                    { new Guid("9e6319d2-ebb1-4502-be32-2025789fbfe4"), "AN", "Консультации по части  ДП" },
                    { new Guid("c8d62f01-fdb1-48e5-bf40-30a00d61f93a"), "AH", "Защита КП(Р)" },
                    { new Guid("dfe740dc-3f68-446b-b2af-e6ea2221f802"), "P", "Лекции" },
                    { new Guid("e3597f2e-56f2-4560-a23a-9ea3da1455fe"), "AA", "Руководство КП(Р)" },
                    { new Guid("edc9d52a-5845-4173-84b5-31a4348a2855"), "AI", "Защита отчета по практике" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("1189dfd3-7275-446e-8be2-5715f979744d"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("127c7662-11ee-4887-8042-0d8b5c1b2fdb"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("169316c8-b05b-4f93-915b-51a3cdfa78e0"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("3f9f3b78-ea23-4f6a-a9d7-d144bec23db8"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("4bf73e1c-08f0-4293-b542-ff59ad344e9a"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("4d45ee78-9b1d-49c8-81fa-e63be9331926"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("56d19396-7fd1-4bb4-b0cd-8f61be9874ef"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("762f1ae4-356d-438b-bbb5-6ee079247731"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("8440bb26-b652-47c0-8844-e0d67216f2aa"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("84b0e2aa-4f8e-4f0f-98e8-d1264a414db8"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("8d6d6cd5-f0e0-4cf9-8e6b-8683a6938f9a"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("8d8b086f-e4a1-4b77-84aa-a1b69941391d"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("9e6319d2-ebb1-4502-be32-2025789fbfe4"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("c8d62f01-fdb1-48e5-bf40-30a00d61f93a"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("dfe740dc-3f68-446b-b2af-e6ea2221f802"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("e3597f2e-56f2-4560-a23a-9ea3da1455fe"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("edc9d52a-5845-4173-84b5-31a4348a2855"));

            migrationBuilder.DropColumn(
                name: "Column",
                table: "Activities");
        }
    }
}
