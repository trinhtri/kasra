using Microsoft.EntityFrameworkCore.Migrations;

namespace GoseiVn.DemoApp.Migrations
{
    public partial class Image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Estimates_EstimatesId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "EstimatesId",
                table: "Images",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Estimates_EstimatesId",
                table: "Images",
                column: "EstimatesId",
                principalTable: "Estimates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Estimates_EstimatesId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "EstimatesId",
                table: "Images",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Estimates_EstimatesId",
                table: "Images",
                column: "EstimatesId",
                principalTable: "Estimates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
