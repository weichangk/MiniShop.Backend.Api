using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.Backend.Api.Migrations
{
    public partial class UpdatePurchaseReceiveOder20220527 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceiveOder_Store_StoreId",
                table: "PurchaseReceiveOder");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReceiveOder_StoreId",
                table: "PurchaseReceiveOder");

            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "PurchaseReceiveOderItem");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "PurchaseReceiveOder");

            migrationBuilder.AddColumn<decimal>(
                name: "RealPurchasePrice",
                table: "PurchaseReceiveOderItem",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "PurchaseReceiveOderItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurchaseOderNo",
                table: "PurchaseReceiveOder",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RealPurchasePrice",
                table: "PurchaseReceiveOderItem");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "PurchaseReceiveOderItem");

            migrationBuilder.DropColumn(
                name: "PurchaseOderNo",
                table: "PurchaseReceiveOder");

            migrationBuilder.AddColumn<decimal>(
                name: "PurchasePrice",
                table: "PurchaseReceiveOderItem",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "PurchaseReceiveOder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceiveOder_StoreId",
                table: "PurchaseReceiveOder",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceiveOder_Store_StoreId",
                table: "PurchaseReceiveOder",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
