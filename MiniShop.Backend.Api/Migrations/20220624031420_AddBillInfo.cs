using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.Backend.Api.Migrations
{
    public partial class AddBillInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<Guid>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: false),
                    OperatorName = table.Column<string>(nullable: true),
                    BillNo = table.Column<string>(nullable: true),
                    SaleWay = table.Column<int>(nullable: false),
                    SaleMoney = table.Column<decimal>(nullable: false),
                    PayMoney = table.Column<decimal>(nullable: false),
                    MemberId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayFlow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<Guid>(nullable: false),
                    BillNo = table.Column<string>(nullable: true),
                    PaymentId = table.Column<int>(nullable: false),
                    PayMoney = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayFlow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayFlow_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleFlow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<Guid>(nullable: false),
                    BillNo = table.Column<string>(nullable: true),
                    ItemId = table.Column<int>(nullable: false),
                    Qty = table.Column<decimal>(nullable: false),
                    SalePrice = table.Column<decimal>(nullable: false),
                    SaleMoney = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleFlow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleFlow_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillInfo_ShopId_BillNo",
                table: "BillInfo",
                columns: new[] { "ShopId", "BillNo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PayFlow_PaymentId",
                table: "PayFlow",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleFlow_ItemId",
                table: "SaleFlow",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillInfo");

            migrationBuilder.DropTable(
                name: "PayFlow");

            migrationBuilder.DropTable(
                name: "SaleFlow");
        }
    }
}
