using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.Backend.Api.Migrations
{
    public partial class UpdatePurchaseReceiveAndReturnOder20220617 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseReceiveOderNo",
                table: "PurchaseReturnOder");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseOderId",
                table: "PurchaseReturnOder",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PurchaseOderNo",
                table: "PurchaseReturnOder",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PurchaseOderId",
                table: "PurchaseReceiveOder",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseOderId",
                table: "PurchaseReturnOder");

            migrationBuilder.DropColumn(
                name: "PurchaseOderNo",
                table: "PurchaseReturnOder");

            migrationBuilder.DropColumn(
                name: "PurchaseOderId",
                table: "PurchaseReceiveOder");

            migrationBuilder.AddColumn<string>(
                name: "PurchaseReceiveOderNo",
                table: "PurchaseReturnOder",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
