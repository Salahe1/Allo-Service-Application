using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allo_Service.Migrations
{
    public partial class addingphotoMetier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Metiers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Metiers");
        }
    }
}
