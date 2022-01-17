using Microsoft.EntityFrameworkCore.Migrations;

namespace FClub.Data.Migrations
{
    public partial class addPropToActivittyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Activities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Activities");
        }
    }
}
