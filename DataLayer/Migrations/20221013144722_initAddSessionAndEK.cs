using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class initAddSessionAndEK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EKs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EKScore = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdStudentId = table.Column<int>(type: "int", nullable: true),
                    IdDisciplineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EKs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EKs_Discipline_IdDisciplineId",
                        column: x => x.IdDisciplineId,
                        principalTable: "Discipline",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EKs_Users_IdStudentId",
                        column: x => x.IdStudentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sessionScores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDisciplineId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScoreSession = table.Column<int>(type: "int", nullable: false),
                    IdStudentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sessionScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sessionScores_Discipline_IdDisciplineId",
                        column: x => x.IdDisciplineId,
                        principalTable: "Discipline",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sessionScores_Users_IdStudentId",
                        column: x => x.IdStudentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EKs_IdDisciplineId",
                table: "EKs",
                column: "IdDisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_EKs_IdStudentId",
                table: "EKs",
                column: "IdStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_sessionScores_IdDisciplineId",
                table: "sessionScores",
                column: "IdDisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_sessionScores_IdStudentId",
                table: "sessionScores",
                column: "IdStudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EKs");

            migrationBuilder.DropTable(
                name: "sessionScores");
        }
    }
}
