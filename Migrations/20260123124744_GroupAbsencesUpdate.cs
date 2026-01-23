using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PoliNote.Migrations
{
    /// <inheritdoc />
    public partial class GroupAbsencesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("27836253-701c-442c-a33e-078b1f810dd8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7b391d2a-4b8d-40c2-8a20-770638636c54"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9205b050-c058-49dc-9e92-88b1a6f6bd97"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b8ea02a9-00e7-4267-a18d-f773710270c3"));

            migrationBuilder.AlterColumn<int>(
                name: "Absences",
                table: "Enrollments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("008fa50a-3668-4146-89a7-d7babe04551b"), "Anna", "Wiśniewska", "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka", "Informant", "informator2" },
                    { new Guid("11756953-b33b-4b7a-ab9d-332d7f3d2709"), "Marek", "Nowak", "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka", "Informant", "informator1" },
                    { new Guid("6e866e55-b0da-4fa8-ab0c-d1b4897f1afd"), "Jan", "Kowalski", "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka", "Admin", "admin" },
                    { new Guid("eef3df25-5295-440a-943b-dcfa83ca2824"), null, null, "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka", "Student", "testuser" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("008fa50a-3668-4146-89a7-d7babe04551b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11756953-b33b-4b7a-ab9d-332d7f3d2709"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6e866e55-b0da-4fa8-ab0c-d1b4897f1afd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("eef3df25-5295-440a-943b-dcfa83ca2824"));

            migrationBuilder.AlterColumn<int>(
                name: "Absences",
                table: "Enrollments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("27836253-701c-442c-a33e-078b1f810dd8"), null, null, "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka", "Student", "testuser" },
                    { new Guid("7b391d2a-4b8d-40c2-8a20-770638636c54"), "Marek", "Nowak", "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka", "Informant", "informator1" },
                    { new Guid("9205b050-c058-49dc-9e92-88b1a6f6bd97"), "Jan", "Kowalski", "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka", "Admin", "admin" },
                    { new Guid("b8ea02a9-00e7-4267-a18d-f773710270c3"), "Anna", "Wiśniewska", "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka", "Informant", "informator2" }
                });
        }
    }
}
