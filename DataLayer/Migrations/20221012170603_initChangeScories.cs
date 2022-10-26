using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class initChangeScories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Score_Lesson_LessonId",
                table: "Score");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "Score",
                newName: "IdLessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Score_LessonId",
                table: "Score",
                newName: "IX_Score_IdLessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Score_Lesson_IdLessonId",
                table: "Score",
                column: "IdLessonId",
                principalTable: "Lesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Score_Lesson_IdLessonId",
                table: "Score");

            migrationBuilder.RenameColumn(
                name: "IdLessonId",
                table: "Score",
                newName: "LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Score_IdLessonId",
                table: "Score",
                newName: "IX_Score_LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Score_Lesson_LessonId",
                table: "Score",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
