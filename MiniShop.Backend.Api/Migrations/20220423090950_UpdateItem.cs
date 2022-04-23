using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.Backend.Api.Migrations
{
    public partial class UpdateItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Supplier_SupplierId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_SupplierId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Item");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Item_SupplierId",
                table: "Item",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Supplier_SupplierId",
                table: "Item",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
