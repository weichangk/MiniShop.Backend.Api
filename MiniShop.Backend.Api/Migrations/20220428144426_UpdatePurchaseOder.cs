using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.Backend.Api.Migrations
{
    public partial class UpdatePurchaseOder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OderNo",
                table: "PurchaseOderItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OderNo",
                table: "PurchaseOderItem",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
