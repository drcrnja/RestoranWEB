using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restoran.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DodajAktivan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gost",
                columns: table => new
                {
                    IDGosta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeGosta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrezimeGosta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gost", x => x.IDGosta);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uloga = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sto",
                columns: table => new
                {
                    IDStola = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojStola = table.Column<int>(type: "int", nullable: false),
                    BrojMesta = table.Column<int>(type: "int", nullable: false),
                    Lokacija = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sto", x => x.IDStola);
                });

            migrationBuilder.CreateTable(
                name: "Rezervacija",
                columns: table => new
                {
                    IDRezervacije = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Vreme = table.Column<TimeSpan>(type: "time", nullable: false),
                    BrojOsoba = table.Column<int>(type: "int", nullable: false),
                    IDGosta = table.Column<int>(type: "int", nullable: false),
                    IDStola = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacija", x => x.IDRezervacije);
                    table.ForeignKey(
                        name: "FK_Rezervacija_Gost_IDGosta",
                        column: x => x.IDGosta,
                        principalTable: "Gost",
                        principalColumn: "IDGosta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezervacija_Sto_IDStola",
                        column: x => x.IDStola,
                        principalTable: "Sto",
                        principalColumn: "IDStola",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_IDGosta",
                table: "Rezervacija",
                column: "IDGosta");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_IDStola",
                table: "Rezervacija",
                column: "IDStola");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Rezervacija");

            migrationBuilder.DropTable(
                name: "Gost");

            migrationBuilder.DropTable(
                name: "Sto");
        }
    }
}
