using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PoliNote.Migrations
{
    /// <inheritdoc />
    public partial class NoteUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a1bbc49-a3ab-487e-ad6a-d1bbad837781"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("95d4ef47-24af-4029-ae58-811814f22cbb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c115f312-42e1-4a94-8b52-a19fcf303f5d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e3be21fc-1d8e-468b-896e-391e3123343f"));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "TargetDate",
                table: "Notes",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("9409fec5-a73d-4368-845f-413a7e619fe0"), null, null, "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka", "Student", "testuser" },
                    { new Guid("9c52922d-b2f4-46b8-81a6-3d22ea382fc3"), "Marek", "Nowak", "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka", "Informant", "informator1" },
                    { new Guid("e5a414af-256f-4fae-bc3a-8be6bc47fc9b"), "Anna", "Wiśniewska", "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka", "Informant", "informator2" },
                    { new Guid("e913526f-8cc7-40a7-a375-1fa40bb1b6fa"), "Jan", "Kowalski", "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka", "Admin", "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9409fec5-a73d-4368-845f-413a7e619fe0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9c52922d-b2f4-46b8-81a6-3d22ea382fc3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e5a414af-256f-4fae-bc3a-8be6bc47fc9b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e913526f-8cc7-40a7-a375-1fa40bb1b6fa"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TargetDate",
                table: "Notes",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("8a1bbc49-a3ab-487e-ad6a-d1bbad837781"), "Anna", "Wiśniewska", "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka", "Informant", "informator2" },
                    { new Guid("95d4ef47-24af-4029-ae58-811814f22cbb"), null, null, "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka", "Student", "testuser" },
                    { new Guid("c115f312-42e1-4a94-8b52-a19fcf303f5d"), "Jan", "Kowalski", "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka", "Admin", "admin" },
                    { new Guid("e3be21fc-1d8e-468b-896e-391e3123343f"), "Marek", "Nowak", "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka", "Informant", "informator1" }
                });
        }
    }
}
