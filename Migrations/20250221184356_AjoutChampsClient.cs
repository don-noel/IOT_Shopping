using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOT_Shopping.Migrations
{
    public partial class AjoutChampsClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdresseClient",
                table: "Commandes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateNaissanceClient",
                table: "Commandes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NomClient",
                table: "Commandes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrenomClient",
                table: "Commandes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdresseClient",
                table: "Commandes");

            migrationBuilder.DropColumn(
                name: "DateNaissanceClient",
                table: "Commandes");

            migrationBuilder.DropColumn(
                name: "NomClient",
                table: "Commandes");

            migrationBuilder.DropColumn(
                name: "PrenomClient",
                table: "Commandes");
        }
    }
}
