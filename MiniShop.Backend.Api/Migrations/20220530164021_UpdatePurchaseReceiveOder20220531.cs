using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.Backend.Api.Migrations
{
    public partial class UpdatePurchaseReceiveOder20220531 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "PurchaseReceiveOderItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "PurchaseReceiveOderItem",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
