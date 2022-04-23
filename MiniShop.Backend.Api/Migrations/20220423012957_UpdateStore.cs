using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.Backend.Api.Migrations
{
    public partial class UpdateStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Store",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Store");
        }
    }
}
