using Microsoft.EntityFrameworkCore.Migrations;

namespace FClub.Data.Migrations
{
    public partial class addActivittyDaysTableandWeekDaysTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeekDays",
                table: "Activities");

            migrationBuilder.CreateTable(
                name: "WeekDays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeekDay = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivittyDays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivittyId = table.Column<int>(nullable: false),
                    WeekDayId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivittyDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivittyDays_Activities_ActivittyId",
                        column: x => x.ActivittyId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivittyDays_WeekDays_WeekDayId",
                        column: x => x.WeekDayId,
                        principalTable: "WeekDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivittyDays_ActivittyId",
                table: "ActivittyDays",
                column: "ActivittyId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivittyDays_WeekDayId",
                table: "ActivittyDays",
                column: "WeekDayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivittyDays");

            migrationBuilder.DropTable(
                name: "WeekDays");

            migrationBuilder.AddColumn<int>(
                name: "WeekDays",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
