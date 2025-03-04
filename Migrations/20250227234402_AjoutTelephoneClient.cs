using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOT_Shopping.Migrations
{
    public partial class AjoutTelephoneClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TelephoneClient",
                table: "Commandes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TelephoneClient",
                table: "Commandes");
        }
    }
}
