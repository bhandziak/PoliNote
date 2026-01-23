using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PoliNote.Migrations
{
    /// <inheritdoc />
    public partial class GroupUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2c59e8de-67ae-4b7e-9e09-cdfc87f35a12"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("992b8dc0-be1b-4871-8a3e-4a868b1acace"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a7634cbf-e71c-464b-909c-3ae8575f0a2b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("acec1d65-e319-4f7a-b094-2c2f45e9c1b8"));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "LastOccurrence",
                table: "SubjectGroups",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FirstOccurrence",
                table: "SubjectGroups",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastOccurrence",
                table: "SubjectGroups",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FirstOccurrence",
                table: "SubjectGroups",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("2c59e8de-67ae-4b7e-9e09-cdfc87f35a12"), "Marek", "Nowak", "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka", "Informant", "informator1" },
                    { new Guid("992b8dc0-be1b-4871-8a3e-4a868b1acace"), "Jan", "Kowalski", "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka", "Admin", "admin" },
                    { new Guid("a7634cbf-e71c-464b-909c-3ae8575f0a2b"), "Anna", "Wiśniewska", "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka", "Informant", "informator2" },
                    { new Guid("acec1d65-e319-4f7a-b094-2c2f45e9c1b8"), null, null, "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka", "Student", "testuser" }
                });
        }
    }
}
