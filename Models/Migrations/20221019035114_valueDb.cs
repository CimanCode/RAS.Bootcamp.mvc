using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RAS.Bootcamp.mvc.Models.Migrations
{
    public partial class valueDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Password = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    NoHandPhone = table.Column<int>(type: "integer", maxLength: 20, nullable: false),
                    tipe = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "Pembelies",
                columns: table => new
                {
                    IdPembeli = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUser = table.Column<int>(type: "integer", nullable: false),
                    Nama = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    No_HandPhone = table.Column<string>(type: "text", nullable: true),
                    Alamat = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pembelies", x => x.IdPembeli);
                    table.ForeignKey(
                        name: "FK_Pembelies_User_IdUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Penjual",
                columns: table => new
                {
                    IdPenjual = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUser = table.Column<int>(type: "integer", nullable: false),
                    Nama_Toko = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Alamat = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penjual", x => x.IdPenjual);
                    table.ForeignKey(
                        name: "FK_Penjual_User_IdUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Barang",
                columns: table => new
                {
                    IdBarang = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    Nama = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Harga = table.Column<int>(type: "integer", nullable: false),
                    stok = table.Column<int>(type: "integer", nullable: false),
                    IdPenjual = table.Column<int>(type: "integer", nullable: false),
                    FileName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    URL = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barang", x => x.IdBarang);
                    table.ForeignKey(
                        name: "FK_Barang_Penjual_IdPenjual",
                        column: x => x.IdPenjual,
                        principalTable: "Penjual",
                        principalColumn: "IdPenjual",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Barang_IdPenjual",
                table: "Barang",
                column: "IdPenjual");

            migrationBuilder.CreateIndex(
                name: "IX_Pembelies_IdUser",
                table: "Pembelies",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Penjual_IdUser",
                table: "Penjual",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Barang");

            migrationBuilder.DropTable(
                name: "Pembelies");

            migrationBuilder.DropTable(
                name: "Penjual");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
