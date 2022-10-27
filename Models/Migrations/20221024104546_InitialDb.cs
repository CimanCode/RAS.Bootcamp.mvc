using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RAS.Bootcamp.mvc.Models.Migrations
{
    public partial class InitialDb : Migration
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
                name: "Transaksis",
                columns: table => new
                {
                    IdTransaksi = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUser = table.Column<int>(type: "integer", nullable: false),
                    MetodePembayaran = table.Column<string>(type: "text", nullable: false),
                    StatusTransaksi = table.Column<string>(type: "text", nullable: false),
                    StatusBayar = table.Column<string>(type: "text", nullable: false),
                    AlamatPengiriman = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaksis", x => x.IdTransaksi);
                    table.ForeignKey(
                        name: "FK_Transaksis_User_IdUser",
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
                    FileName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    URL = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ItemTransaksis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdBarang = table.Column<int>(type: "integer", nullable: false),
                    Harga = table.Column<decimal>(type: "numeric", nullable: false),
                    Jumlah = table.Column<int>(type: "integer", nullable: false),
                    SubTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    IdTransaksi = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTransaksis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemTransaksis_Barang_IdBarang",
                        column: x => x.IdBarang,
                        principalTable: "Barang",
                        principalColumn: "IdBarang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemTransaksis_Transaksis_IdTransaksi",
                        column: x => x.IdTransaksi,
                        principalTable: "Transaksis",
                        principalColumn: "IdTransaksi",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Keranjangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdBarang = table.Column<int>(type: "integer", nullable: false),
                    HargaSatuan = table.Column<decimal>(type: "numeric", nullable: false),
                    IdUser = table.Column<int>(type: "integer", nullable: false),
                    Jumlah = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keranjangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Keranjangs_Barang_IdBarang",
                        column: x => x.IdBarang,
                        principalTable: "Barang",
                        principalColumn: "IdBarang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Keranjangs_User_IdUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Barang_IdPenjual",
                table: "Barang",
                column: "IdPenjual");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTransaksis_IdBarang",
                table: "ItemTransaksis",
                column: "IdBarang");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTransaksis_IdTransaksi",
                table: "ItemTransaksis",
                column: "IdTransaksi");

            migrationBuilder.CreateIndex(
                name: "IX_Keranjangs_IdBarang",
                table: "Keranjangs",
                column: "IdBarang");

            migrationBuilder.CreateIndex(
                name: "IX_Keranjangs_IdUser",
                table: "Keranjangs",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Pembelies_IdUser",
                table: "Pembelies",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Penjual_IdUser",
                table: "Penjual",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Transaksis_IdUser",
                table: "Transaksis",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemTransaksis");

            migrationBuilder.DropTable(
                name: "Keranjangs");

            migrationBuilder.DropTable(
                name: "Pembelies");

            migrationBuilder.DropTable(
                name: "Transaksis");

            migrationBuilder.DropTable(
                name: "Barang");

            migrationBuilder.DropTable(
                name: "Penjual");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
