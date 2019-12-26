using Microsoft.EntityFrameworkCore.Migrations;

namespace GoseiVn.DemoApp.Migrations
{
    public partial class Alter_Estimate_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estimates_States_StatesId",
                table: "Estimates");

            migrationBuilder.DropIndex(
                name: "IX_Estimates_StatesId",
                table: "Estimates");

            migrationBuilder.DropColumn(
                name: "StatesId",
                table: "Estimates");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "Estimates",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Estimates_StateId",
                table: "Estimates",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estimates_States_StateId",
                table: "Estimates",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estimates_States_StateId",
                table: "Estimates");

            migrationBuilder.DropIndex(
                name: "IX_Estimates_StateId",
                table: "Estimates");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "Estimates",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatesId",
                table: "Estimates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estimates_StatesId",
                table: "Estimates",
                column: "StatesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estimates_States_StatesId",
                table: "Estimates",
                column: "StatesId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
