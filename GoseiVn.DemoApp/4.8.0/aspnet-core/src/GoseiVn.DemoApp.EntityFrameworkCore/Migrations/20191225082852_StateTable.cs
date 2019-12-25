using Microsoft.EntityFrameworkCore.Migrations;

namespace GoseiVn.DemoApp.Migrations
{
    public partial class StateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Estimates",
                newName: "StateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "Estimates",
                newName: "State");
        }
    }
}
