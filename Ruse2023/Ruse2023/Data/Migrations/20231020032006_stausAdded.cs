using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ruse2023.Data.Migrations
{
    public partial class stausAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "TreePlantApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ShoppingApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "TreePlantApplications");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ShoppingApplications");
        }
    }
}
