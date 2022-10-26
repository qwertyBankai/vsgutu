using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "years",
                table: "Discipline",
                newName: "yearsStart");

            migrationBuilder.AddColumn<DateTime>(
                name: "yearsEnd",
                table: "Discipline",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "yearsEnd",
                table: "Discipline");

            migrationBuilder.RenameColumn(
                name: "yearsStart",
                table: "Discipline",
                newName: "years");
        }
    }
}
