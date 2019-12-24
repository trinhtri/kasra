using Microsoft.EntityFrameworkCore.Migrations;

namespace GoseiVn.DemoApp.Migrations
{
    public partial class update_image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimateID",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Images",
                type: "NVARCHAR(500)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "EstimateID",
                table: "Images",
                nullable: false,
                defaultValue: 0);
        }
    }
}
