using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Querying.Migrations
{
    public partial class mig_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Urunler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fiyat = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urunler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parcalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParcaAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrunId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcalar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcalar_Urunler_UrunId",
                        column: x => x.UrunId,
                        principalTable: "Urunler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UrunParca",
                columns: table => new
                {
                    UrunId = table.Column<int>(type: "int", nullable: false),
                    ParcaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunParca", x => new { x.UrunId, x.ParcaId });
                    table.ForeignKey(
                        name: "FK_UrunParca_Parcalar_ParcaId",
                        column: x => x.ParcaId,
                        principalTable: "Parcalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UrunParca_Urunler_UrunId",
                        column: x => x.UrunId,
                        principalTable: "Urunler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parcalar_UrunId",
                table: "Parcalar",
                column: "UrunId");

            migrationBuilder.CreateIndex(
                name: "IX_UrunParca_ParcaId",
                table: "UrunParca",
                column: "ParcaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrunParca");

            migrationBuilder.DropTable(
                name: "Parcalar");

            migrationBuilder.DropTable(
                name: "Urunler");
        }
    }
}
