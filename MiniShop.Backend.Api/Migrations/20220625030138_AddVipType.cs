using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.Backend.Api.Migrations
{
    public partial class AddVipType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VipType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ShopId = table.Column<Guid>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: false),
                    OperatorName = table.Column<string>(nullable: true),
                    Code = table.Column<int>(nullable: false),
                    DiscountWay = table.Column<int>(nullable: false),
                    DiscountRate = table.Column<decimal>(nullable: false),
                    IsScore = table.Column<int>(nullable: false),
                    IsStore = table.Column<int>(nullable: false),
                    RequirementScore = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VipType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VipType_ShopId_Code",
                table: "VipType",
                columns: new[] { "ShopId", "Code" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VipType");
        }
    }
}
