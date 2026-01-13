using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restoran.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DodajNarudzbinuFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NarudzbinaId",
                table: "Rezervacija",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RezervacijaId",
                table: "Narudzbine",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_NarudzbinaId",
                table: "Rezervacija",
                column: "NarudzbinaId",
                unique: true,
                filter: "[NarudzbinaId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_Narudzbine_NarudzbinaId",
                table: "Rezervacija",
                column: "NarudzbinaId",
                principalTable: "Narudzbine",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_Narudzbine_NarudzbinaId",
                table: "Rezervacija");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacija_NarudzbinaId",
                table: "Rezervacija");

            migrationBuilder.DropColumn(
                name: "NarudzbinaId",
                table: "Rezervacija");

            migrationBuilder.DropColumn(
                name: "RezervacijaId",
                table: "Narudzbine");
        }
    }
}
