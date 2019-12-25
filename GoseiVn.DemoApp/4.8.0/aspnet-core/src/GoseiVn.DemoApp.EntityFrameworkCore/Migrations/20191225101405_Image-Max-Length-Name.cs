using Microsoft.EntityFrameworkCore.Migrations;

namespace GoseiVn.DemoApp.Migrations
{
    public partial class ImageMaxLengthName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "Images",
                type: "NVARCHAR(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(20)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "Images",
                type: "NVARCHAR(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(100)");
        }
    }
}
