using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FClub.Data.Migrations
{
    public partial class updateActivittyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnrollmentDate",
                table: "People");

            migrationBuilder.DropColumn(
                name: "PricePerClass",
                table: "Activities");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Enrollments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Enrollments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Enrollments");

            migrationBuilder.AddColumn<string>(
                name: "EnrollmentDate",
                table: "People",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PricePerClass",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
