using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FClub.Data.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    EnrollmentDate = table.Column<string>(nullable: true),
                    About = table.Column<string>(nullable: true),
                    ProfilePicture = table.Column<string>(nullable: true),
                    HireDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FromToPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Period = table.Column<string>(nullable: true),
                    ShiftId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FromToPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FromToPeriods_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivittyName = table.Column<string>(nullable: true),
                    TotalCapacity = table.Column<int>(nullable: false),
                    CurrentCapacity = table.Column<int>(nullable: false),
                    InstructorId = table.Column<int>(nullable: false),
                    WeekDays = table.Column<int>(nullable: false),
                    PricePerClass = table.Column<double>(nullable: false),
                    PricePerMonth = table.Column<double>(nullable: false),
                    PricePer3Months = table.Column<double>(nullable: false),
                    FreePass = table.Column<double>(nullable: false),
                    FromToPeriodId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_FromToPeriods_FromToPeriodId",
                        column: x => x.FromToPeriodId,
                        principalTable: "FromToPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Activities_People_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivittyId = table.Column<int>(nullable: false),
                    AppUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_Activities_ActivittyId",
                        column: x => x.ActivittyId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_People_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_FromToPeriodId",
                table: "Activities",
                column: "FromToPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_InstructorId",
                table: "Activities",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_ActivittyId",
                table: "Enrollments",
                column: "ActivittyId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_AppUserId",
                table: "Enrollments",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FromToPeriods_ShiftId",
                table: "FromToPeriods",
                column: "ShiftId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "FromToPeriods");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Shifts");
        }
    }
}
