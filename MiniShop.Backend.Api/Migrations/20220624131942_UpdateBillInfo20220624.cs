using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.Backend.Api.Migrations
{
    public partial class UpdateBillInfo20220624 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayMoney",
                table: "BillInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PayMoney",
                table: "BillInfo",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
