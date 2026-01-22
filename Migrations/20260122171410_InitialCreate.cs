using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PoliNote.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Etcs = table.Column<int>(type: "integer", nullable: false),
                    SyllabusUrl = table.Column<string>(type: "text", nullable: false),
                    LecturerName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupName = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    TeacherName = table.Column<string>(type: "text", nullable: false),
                    Frequency = table.Column<int>(type: "integer", nullable: false),
                    FirstOccurrence = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastOccurrence = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectGroups_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivateEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: true),
                    EventType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivateEvents_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicEvents_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Absences = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_SubjectGroups_SubjectGroupId",
                        column: x => x.SubjectGroupId,
                        principalTable: "SubjectGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_SubjectGroupId",
                table: "Enrollments",
                column: "SubjectGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_UserId_SubjectGroupId",
                table: "Enrollments",
                columns: new[] { "UserId", "SubjectGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrivateEvents_CreatedByUserId",
                table: "PrivateEvents",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicEvents_CreatedByUserId",
                table: "PublicEvents",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGroups_SubjectId",
                table: "SubjectGroups",
                column: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "PrivateEvents");

            migrationBuilder.DropTable(
                name: "PublicEvents");

            migrationBuilder.DropTable(
                name: "SubjectGroups");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
