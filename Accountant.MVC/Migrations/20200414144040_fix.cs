using Microsoft.EntityFrameworkCore.Migrations;

namespace Accountant.MVC.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ESpendingType",
                table: "ESpendingType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EIncomeType",
                table: "EIncomeType");

            migrationBuilder.RenameTable(
                name: "ESpendingType",
                newName: "SpendingTypes");

            migrationBuilder.RenameTable(
                name: "EIncomeType",
                newName: "IncomeTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpendingTypes",
                table: "SpendingTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IncomeTypes",
                table: "IncomeTypes",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SpendingTypes",
                table: "SpendingTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IncomeTypes",
                table: "IncomeTypes");

            migrationBuilder.RenameTable(
                name: "SpendingTypes",
                newName: "ESpendingType");

            migrationBuilder.RenameTable(
                name: "IncomeTypes",
                newName: "EIncomeType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ESpendingType",
                table: "ESpendingType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EIncomeType",
                table: "EIncomeType",
                column: "Id");
        }
    }
}
