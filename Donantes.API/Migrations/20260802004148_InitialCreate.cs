using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Donantes.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Donadores",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Dni = table.Column<string>(type: "TEXT", nullable: false),
                    BloodType = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Available = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesSangre",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    BloodType = table.Column<string>(type: "TEXT", nullable: false),
                    Hospital = table.Column<string>(type: "TEXT", nullable: false),
                    Contact = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<bool>(type: "INTEGER", nullable: false),
                    DonadorId = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesSangre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudesSangre_Donadores_DonadorId",
                        column: x => x.DonadorId,
                        principalTable: "Donadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donadores_Dni",
                table: "Donadores",
                column: "Dni",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesSangre_DonadorId",
                table: "SolicitudesSangre",
                column: "DonadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitudesSangre");

            migrationBuilder.DropTable(
                name: "Donadores");
        }
    }
}
