using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolesOfUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesOfUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeLessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeLessons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdGroupId = table.Column<int>(type: "int", nullable: true),
                    IdRoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Groups_IdGroupId",
                        column: x => x.IdGroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_RolesOfUsers_IdRoleId",
                        column: x => x.IdRoleId,
                        principalTable: "RolesOfUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Discipline",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    years = table.Column<DateTime>(type: "datetime2", nullable: false),
                    block = table.Column<int>(type: "int", nullable: false),
                    zet = table.Column<int>(type: "int", nullable: false),
                    formAttestation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    availabilityOfCoursework = table.Column<bool>(type: "bit", nullable: false),
                    IdGroupId = table.Column<int>(type: "int", nullable: true),
                    IdTeacherId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discipline", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discipline_Groups_IdGroupId",
                        column: x => x.IdGroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Discipline_Users_IdTeacherId",
                        column: x => x.IdTeacherId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameLesson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdDisciplineId = table.Column<int>(type: "int", nullable: true),
                    IdTypeLessonId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lesson_Discipline_IdDisciplineId",
                        column: x => x.IdDisciplineId,
                        principalTable: "Discipline",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lesson_TypeLessons_IdTypeLessonId",
                        column: x => x.IdTypeLessonId,
                        principalTable: "TypeLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Evalution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdStudentId = table.Column<int>(type: "int", nullable: true),
                    Attendance = table.Column<bool>(type: "bit", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Score_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Score_Users_IdStudentId",
                        column: x => x.IdStudentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discipline_IdGroupId",
                table: "Discipline",
                column: "IdGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Discipline_IdTeacherId",
                table: "Discipline",
                column: "IdTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_IdDisciplineId",
                table: "Lesson",
                column: "IdDisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_IdTypeLessonId",
                table: "Lesson",
                column: "IdTypeLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Score_IdStudentId",
                table: "Score",
                column: "IdStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Score_LessonId",
                table: "Score",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdGroupId",
                table: "Users",
                column: "IdGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdRoleId",
                table: "Users",
                column: "IdRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Score");

            migrationBuilder.DropTable(
                name: "Lesson");

            migrationBuilder.DropTable(
                name: "Discipline");

            migrationBuilder.DropTable(
                name: "TypeLessons");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "RolesOfUsers");
        }
    }
}
