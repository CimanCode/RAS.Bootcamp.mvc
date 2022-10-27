using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RAS.Bootcamp.mvc.Models.Migrations
{
    public partial class valueDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalHarga",
                table: "Transaksis",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalHarga",
                table: "Transaksis");
        }
    }
}
