using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.Backend.Api.Migrations
{
    public partial class updatePurchaseOder1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseOderId",
                table: "PurchaseOderItem");

            migrationBuilder.AddColumn<string>(
                name: "OderNo",
                table: "PurchaseOderItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OderNo",
                table: "PurchaseOderItem");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseOderId",
                table: "PurchaseOderItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
