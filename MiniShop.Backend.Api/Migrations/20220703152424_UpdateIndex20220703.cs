using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.Backend.Api.Migrations
{
    public partial class UpdateIndex20220703 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BillNo",
                table: "SaleFlow",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BillNo",
                table: "PayFlow",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VipScoreSetting_Id_ShopId",
                table: "VipScoreSetting",
                columns: new[] { "Id", "ShopId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stock_Id_ShopId",
                table: "Stock",
                columns: new[] { "Id", "ShopId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleFlow_ShopId_BillNo",
                table: "SaleFlow",
                columns: new[] { "ShopId", "BillNo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnOderItem_Id_ShopId",
                table: "PurchaseReturnOderItem",
                columns: new[] { "Id", "ShopId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceiveOderItem_Id_ShopId",
                table: "PurchaseReceiveOderItem",
                columns: new[] { "Id", "ShopId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOderItem_Id_ShopId",
                table: "PurchaseOderItem",
                columns: new[] { "Id", "ShopId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromotionSpecialOfferItem_Id_ShopId",
                table: "PromotionSpecialOfferItem",
                columns: new[] { "Id", "ShopId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDiscountItem_Id_ShopId",
                table: "PromotionDiscountItem",
                columns: new[] { "Id", "ShopId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDiscountCategorie_Id_ShopId",
                table: "PromotionDiscountCategorie",
                columns: new[] { "Id", "ShopId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PayFlow_ShopId_BillNo",
                table: "PayFlow",
                columns: new[] { "ShopId", "BillNo" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VipScoreSetting_Id_ShopId",
                table: "VipScoreSetting");

            migrationBuilder.DropIndex(
                name: "IX_Stock_Id_ShopId",
                table: "Stock");

            migrationBuilder.DropIndex(
                name: "IX_SaleFlow_ShopId_BillNo",
                table: "SaleFlow");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReturnOderItem_Id_ShopId",
                table: "PurchaseReturnOderItem");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReceiveOderItem_Id_ShopId",
                table: "PurchaseReceiveOderItem");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOderItem_Id_ShopId",
                table: "PurchaseOderItem");

            migrationBuilder.DropIndex(
                name: "IX_PromotionSpecialOfferItem_Id_ShopId",
                table: "PromotionSpecialOfferItem");

            migrationBuilder.DropIndex(
                name: "IX_PromotionDiscountItem_Id_ShopId",
                table: "PromotionDiscountItem");

            migrationBuilder.DropIndex(
                name: "IX_PromotionDiscountCategorie_Id_ShopId",
                table: "PromotionDiscountCategorie");

            migrationBuilder.DropIndex(
                name: "IX_PayFlow_ShopId_BillNo",
                table: "PayFlow");

            migrationBuilder.AlterColumn<string>(
                name: "BillNo",
                table: "SaleFlow",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BillNo",
                table: "PayFlow",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
