using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.Backend.Api.Migrations
{
    public partial class UpdatePurchaseOder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOder_Store_StoreId",
                table: "PurchaseOder");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOder_Supplier_SupplierId",
                table: "PurchaseOder");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOderItem_Item_ItemId",
                table: "PurchaseOderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOderItem_PurchaseOder_PurchaseOderId",
                table: "PurchaseOderItem");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOderItem_ItemId",
                table: "PurchaseOderItem");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOderItem_PurchaseOderId",
                table: "PurchaseOderItem");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOder_StoreId",
                table: "PurchaseOder");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOder_SupplierId",
                table: "PurchaseOder");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "PurchaseOder");

            migrationBuilder.AddColumn<string>(
                name: "ItemCode",
                table: "PurchaseOderItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "PurchaseOderItem",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RealPurchasePrice",
                table: "PurchaseOderItem",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "PurchaseOderItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupplierName",
                table: "PurchaseOder",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemCode",
                table: "PurchaseOderItem");

            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "PurchaseOderItem");

            migrationBuilder.DropColumn(
                name: "RealPurchasePrice",
                table: "PurchaseOderItem");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "PurchaseOderItem");

            migrationBuilder.DropColumn(
                name: "SupplierName",
                table: "PurchaseOder");

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "PurchaseOder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOderItem_ItemId",
                table: "PurchaseOderItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOderItem_PurchaseOderId",
                table: "PurchaseOderItem",
                column: "PurchaseOderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOder_StoreId",
                table: "PurchaseOder",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOder_SupplierId",
                table: "PurchaseOder",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOder_Store_StoreId",
                table: "PurchaseOder",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOder_Supplier_SupplierId",
                table: "PurchaseOder",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOderItem_Item_ItemId",
                table: "PurchaseOderItem",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOderItem_PurchaseOder_PurchaseOderId",
                table: "PurchaseOderItem",
                column: "PurchaseOderId",
                principalTable: "PurchaseOder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
