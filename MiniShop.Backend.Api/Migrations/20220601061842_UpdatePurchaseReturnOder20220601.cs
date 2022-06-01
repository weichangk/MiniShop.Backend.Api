using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.Backend.Api.Migrations
{
    public partial class UpdatePurchaseReturnOder20220601 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnOder_Store_StoreId",
                table: "PurchaseReturnOder");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReturnOder_StoreId",
                table: "PurchaseReturnOder");

            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "PurchaseReturnOderItem");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "PurchaseReturnOderItem");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "PurchaseReturnOder");

            migrationBuilder.AddColumn<decimal>(
                name: "RealPurchasePrice",
                table: "PurchaseReturnOderItem",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "PurchaseReturnOderItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderState",
                table: "PurchaseReturnOder",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PurchaseReceiveOderNo",
                table: "PurchaseReturnOder",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RealPurchasePrice",
                table: "PurchaseReturnOderItem");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "PurchaseReturnOderItem");

            migrationBuilder.DropColumn(
                name: "OrderState",
                table: "PurchaseReturnOder");

            migrationBuilder.DropColumn(
                name: "PurchaseReceiveOderNo",
                table: "PurchaseReturnOder");

            migrationBuilder.AddColumn<decimal>(
                name: "PurchasePrice",
                table: "PurchaseReturnOderItem",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "PurchaseReturnOderItem",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "PurchaseReturnOder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnOder_StoreId",
                table: "PurchaseReturnOder",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnOder_Store_StoreId",
                table: "PurchaseReturnOder",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
