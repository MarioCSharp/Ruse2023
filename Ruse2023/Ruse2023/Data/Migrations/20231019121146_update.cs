using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ruse2023.Data.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "ModeratorApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "ModeratorApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "ModeratorApplications");

            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "ModeratorApplications");
        }
    }
}
