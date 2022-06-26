using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.Backend.Api.Migrations
{
    public partial class AddPromotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PromotionOder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ShopId = table.Column<Guid>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: false),
                    OperatorName = table.Column<string>(nullable: true),
                    OderNo = table.Column<string>(nullable: true),
                    VipTypeId = table.Column<int>(nullable: false),
                    PromotionType = table.Column<int>(nullable: false),
                    SpecialOfferWay = table.Column<int>(nullable: false),
                    SpecialOfferScope = table.Column<int>(nullable: false),
                    DiscountWay = table.Column<int>(nullable: false),
                    DiscountScope = table.Column<int>(nullable: false),
                    DiscountRate = table.Column<decimal>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    AuditState = table.Column<int>(nullable: false),
                    AuditOperatorName = table.Column<string>(nullable: true),
                    AuditTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionOder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionOder_VipType_VipTypeId",
                        column: x => x.VipTypeId,
                        principalTable: "VipType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionDiscountCategorie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<Guid>(nullable: false),
                    PromotionOderId = table.Column<int>(nullable: false),
                    CategorieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionDiscountCategorie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionDiscountCategorie_Categorie_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionDiscountCategorie_PromotionOder_PromotionOderId",
                        column: x => x.PromotionOderId,
                        principalTable: "PromotionOder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionDiscountItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<Guid>(nullable: false),
                    PromotionOderId = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionDiscountItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionDiscountItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionDiscountItem_PromotionOder_PromotionOderId",
                        column: x => x.PromotionOderId,
                        principalTable: "PromotionOder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionSpecialOfferItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<Guid>(nullable: false),
                    PromotionOderId = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    SpecialPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionSpecialOfferItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionSpecialOfferItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionSpecialOfferItem_PromotionOder_PromotionOderId",
                        column: x => x.PromotionOderId,
                        principalTable: "PromotionOder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDiscountCategorie_CategorieId",
                table: "PromotionDiscountCategorie",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDiscountCategorie_PromotionOderId",
                table: "PromotionDiscountCategorie",
                column: "PromotionOderId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDiscountItem_ItemId",
                table: "PromotionDiscountItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDiscountItem_PromotionOderId",
                table: "PromotionDiscountItem",
                column: "PromotionOderId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionOder_VipTypeId",
                table: "PromotionOder",
                column: "VipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionOder_ShopId_OderNo",
                table: "PromotionOder",
                columns: new[] { "ShopId", "OderNo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromotionSpecialOfferItem_ItemId",
                table: "PromotionSpecialOfferItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionSpecialOfferItem_PromotionOderId",
                table: "PromotionSpecialOfferItem",
                column: "PromotionOderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromotionDiscountCategorie");

            migrationBuilder.DropTable(
                name: "PromotionDiscountItem");

            migrationBuilder.DropTable(
                name: "PromotionSpecialOfferItem");

            migrationBuilder.DropTable(
                name: "PromotionOder");
        }
    }
}
