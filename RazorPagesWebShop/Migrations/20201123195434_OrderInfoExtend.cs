using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorPagesWebShop.Migrations
{
    public partial class OrderInfoExtend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "OrderInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "OrderInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseNumber",
                table: "OrderInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "OrderInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "OrderInfo");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "OrderInfo");

            migrationBuilder.DropColumn(
                name: "HouseNumber",
                table: "OrderInfo");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "OrderInfo");
        }
    }
}
