using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.Backend.Api.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: false),
                    OperatorName = table.Column<string>(nullable: true),
                    Code = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    ParentCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shop",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Deleted = table.Column<int>(nullable: false),
                    ShopId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: false),
                    OperatorName = table.Column<string>(nullable: true),
                    Contacts = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ValidDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shop", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Deleted = table.Column<int>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: false),
                    ShopId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: false),
                    OperatorName = table.Column<string>(nullable: true),
                    Contacts = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Deleted = table.Column<int>(nullable: false),
                    ShopId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: false),
                    OperatorName = table.Column<string>(nullable: true),
                    Code = table.Column<int>(nullable: false),
                    Contacts = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: false),
                    OperatorName = table.Column<string>(nullable: true),
                    Code = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vip",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Deleted = table.Column<int>(nullable: false),
                    ShopId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: false),
                    OperatorName = table.Column<string>(nullable: true),
                    VipType = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Sex = table.Column<int>(nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    ValidDate = table.Column<DateTime>(nullable: false),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vip", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<Guid>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: false),
                    OperatorName = table.Column<string>(nullable: true),
                    OderNo = table.Column<string>(nullable: true),
                    SupplierId = table.Column<int>(nullable: false),
                    OderAmount = table.Column<decimal>(nullable: false),
                    AuditState = table.Column<int>(nullable: false),
                    AuditOperatorName = table.Column<string>(nullable: true),
                    AuditTime = table.Column<DateTime>(nullable: true),
                    OrderState = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOder_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReceiveOder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<Guid>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: false),
                    OperatorName = table.Column<string>(nullable: true),
                    OderNo = table.Column<string>(nullable: true),
                    StoreId = table.Column<int>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false),
                    OderAmount = table.Column<decimal>(nullable: false),
                    AuditState = table.Column<int>(nullable: false),
                    AuditOperatorName = table.Column<string>(nullable: true),
                    AuditTime = table.Column<DateTime>(nullable: true),
                    OrderState = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseReceiveOder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseReceiveOder_Store_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseReceiveOder_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReturnOder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<Guid>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: false),
                    OperatorName = table.Column<string>(nullable: true),
                    OderNo = table.Column<string>(nullable: true),
                    StoreId = table.Column<int>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false),
                    OderAmount = table.Column<decimal>(nullable: false),
                    AuditState = table.Column<int>(nullable: false),
                    AuditOperatorName = table.Column<string>(nullable: true),
                    AuditTime = table.Column<DateTime>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseReturnOder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseReturnOder_Store_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseReturnOder_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: false),
                    OperatorName = table.Column<string>(nullable: true),
                    CategorieId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    PurchasePrice = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    PriceType = table.Column<int>(nullable: false),
                    UnitId = table.Column<int>(nullable: false),
                    Picture = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_Categorie_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Item_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOderItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StoreId = table.Column<Guid>(nullable: false),
                    ShopId = table.Column<Guid>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: false),
                    OperatorName = table.Column<string>(nullable: true),
                    PurchaseOderId = table.Column<int>(nullable: false),
                    OderNo = table.Column<string>(nullable: true),
                    ItemId = table.Column<int>(nullable: false),
                    Number = table.Column<decimal>(nullable: false),
                    GiveNumber = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    RealPurchasePrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOderItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOderItem_PurchaseOder_PurchaseOderId",
                        column: x => x.PurchaseOderId,
                        principalTable: "PurchaseOder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReceiveOderItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StoreId = table.Column<Guid>(nullable: false),
                    ShopId = table.Column<Guid>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: false),
                    OperatorName = table.Column<string>(nullable: true),
                    PurchaseReceiveOderId = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    Number = table.Column<decimal>(nullable: false),
                    GiveNumber = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    PurchasePrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseReceiveOderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseReceiveOderItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseReceiveOderItem_PurchaseReceiveOder_PurchaseReceiveO~",
                        column: x => x.PurchaseReceiveOderId,
                        principalTable: "PurchaseReceiveOder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReturnOderItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StoreId = table.Column<Guid>(nullable: false),
                    ShopId = table.Column<Guid>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: false),
                    OperatorName = table.Column<string>(nullable: true),
                    PurchaseReturnOderId = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    Number = table.Column<decimal>(nullable: false),
                    GiveNumber = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    PurchasePrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseReturnOderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseReturnOderItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseReturnOderItem_PurchaseReturnOder_PurchaseReturnOder~",
                        column: x => x.PurchaseReturnOderId,
                        principalTable: "PurchaseReturnOder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorie_ShopId_Code",
                table: "Categorie",
                columns: new[] { "ShopId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_CategorieId",
                table: "Item",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_UnitId",
                table: "Item",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ShopId_Code",
                table: "Item",
                columns: new[] { "ShopId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOder_SupplierId",
                table: "PurchaseOder",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOder_ShopId_OderNo",
                table: "PurchaseOder",
                columns: new[] { "ShopId", "OderNo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOderItem_ItemId",
                table: "PurchaseOderItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOderItem_PurchaseOderId",
                table: "PurchaseOderItem",
                column: "PurchaseOderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceiveOder_StoreId",
                table: "PurchaseReceiveOder",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceiveOder_SupplierId",
                table: "PurchaseReceiveOder",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceiveOder_ShopId_OderNo",
                table: "PurchaseReceiveOder",
                columns: new[] { "ShopId", "OderNo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceiveOderItem_ItemId",
                table: "PurchaseReceiveOderItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceiveOderItem_PurchaseReceiveOderId",
                table: "PurchaseReceiveOderItem",
                column: "PurchaseReceiveOderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnOder_StoreId",
                table: "PurchaseReturnOder",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnOder_SupplierId",
                table: "PurchaseReturnOder",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnOder_ShopId_OderNo",
                table: "PurchaseReturnOder",
                columns: new[] { "ShopId", "OderNo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnOderItem_ItemId",
                table: "PurchaseReturnOderItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnOderItem_PurchaseReturnOderId",
                table: "PurchaseReturnOderItem",
                column: "PurchaseReturnOderId");

            migrationBuilder.CreateIndex(
                name: "IX_Shop_ShopId",
                table: "Shop",
                column: "ShopId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Store_ShopId_Name",
                table: "Store",
                columns: new[] { "ShopId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Store_ShopId_StoreId",
                table: "Store",
                columns: new[] { "ShopId", "StoreId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_ShopId_Code",
                table: "Supplier",
                columns: new[] { "ShopId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unit_ShopId_Code",
                table: "Unit",
                columns: new[] { "ShopId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vip_ShopId_Code",
                table: "Vip",
                columns: new[] { "ShopId", "Code" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseOderItem");

            migrationBuilder.DropTable(
                name: "PurchaseReceiveOderItem");

            migrationBuilder.DropTable(
                name: "PurchaseReturnOderItem");

            migrationBuilder.DropTable(
                name: "Shop");

            migrationBuilder.DropTable(
                name: "Vip");

            migrationBuilder.DropTable(
                name: "PurchaseOder");

            migrationBuilder.DropTable(
                name: "PurchaseReceiveOder");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "PurchaseReturnOder");

            migrationBuilder.DropTable(
                name: "Categorie");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "Supplier");
        }
    }
}
