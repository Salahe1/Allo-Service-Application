using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allo_Service.Migrations
{
    public partial class UpdatingAvisModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Commentaire",
                table: "Avis");

            migrationBuilder.DropColumn(
                name: "Show",
                table: "Avis");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Commentaire",
                table: "Avis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Show",
                table: "Avis",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
