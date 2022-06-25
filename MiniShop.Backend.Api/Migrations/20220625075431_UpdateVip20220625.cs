using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.Backend.Api.Migrations
{
    public partial class UpdateVip20220625 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VipType",
                table: "Vip");

            migrationBuilder.AddColumn<decimal>(
                name: "ConsumeAmount",
                table: "Vip",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ScoreAmount",
                table: "Vip",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "StoreAmount",
                table: "Vip",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "VipTypeId",
                table: "Vip",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vip_VipTypeId",
                table: "Vip",
                column: "VipTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vip_VipType_VipTypeId",
                table: "Vip",
                column: "VipTypeId",
                principalTable: "VipType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vip_VipType_VipTypeId",
                table: "Vip");

            migrationBuilder.DropIndex(
                name: "IX_Vip_VipTypeId",
                table: "Vip");

            migrationBuilder.DropColumn(
                name: "ConsumeAmount",
                table: "Vip");

            migrationBuilder.DropColumn(
                name: "ScoreAmount",
                table: "Vip");

            migrationBuilder.DropColumn(
                name: "StoreAmount",
                table: "Vip");

            migrationBuilder.DropColumn(
                name: "VipTypeId",
                table: "Vip");

            migrationBuilder.AddColumn<int>(
                name: "VipType",
                table: "Vip",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
