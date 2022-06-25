using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.Backend.Api.Migrations
{
    public partial class AddVipScoreSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VipScoreSetting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<Guid>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: false),
                    OperatorName = table.Column<string>(nullable: true),
                    VipTypeId = table.Column<int>(nullable: false),
                    VipScoreWay = table.Column<int>(nullable: false),
                    ConsumeAmount = table.Column<decimal>(nullable: false),
                    ScoreAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VipScoreSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VipScoreSetting_VipType_VipTypeId",
                        column: x => x.VipTypeId,
                        principalTable: "VipType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VipScoreSetting_VipTypeId",
                table: "VipScoreSetting",
                column: "VipTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VipScoreSetting");
        }
    }
}
