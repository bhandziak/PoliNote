using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PoliNote.Migrations
{
    /// <inheritdoc />
    public partial class AddNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_SubjectGroups_SubjectGroupId",
                        column: x => x.SubjectGroupId,
                        principalTable: "SubjectGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Notes_SubjectGroupId",
                table: "Notes",
                column: "SubjectGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UserId_SubjectGroupId_TargetDate",
                table: "Notes",
                columns: new[] { "UserId", "SubjectGroupId", "TargetDate" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

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
    }
}
