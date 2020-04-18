using Microsoft.EntityFrameworkCore.Migrations;

namespace Accountant.MVC.Migrations
{
    public partial class fix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balances_Months_EMonth",
                table: "Balances");

            migrationBuilder.DropIndex(
                name: "IX_Balances_EMonth",
                table: "Balances");

            migrationBuilder.DropColumn(
                name: "EMonth",
                table: "Balances");

            migrationBuilder.AddColumn<int>(
                name: "MonthId",
                table: "Balances",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Balances_MonthId",
                table: "Balances",
                column: "MonthId");

            migrationBuilder.AddForeignKey(
                name: "FK_Balances_Months_MonthId",
                table: "Balances",
                column: "MonthId",
                principalTable: "Months",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balances_Months_MonthId",
                table: "Balances");

            migrationBuilder.DropIndex(
                name: "IX_Balances_MonthId",
                table: "Balances");

            migrationBuilder.DropColumn(
                name: "MonthId",
                table: "Balances");

            migrationBuilder.AddColumn<int>(
                name: "EMonth",
                table: "Balances",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Balances_EMonth",
                table: "Balances",
                column: "EMonth");

            migrationBuilder.AddForeignKey(
                name: "FK_Balances_Months_EMonth",
                table: "Balances",
                column: "EMonth",
                principalTable: "Months",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
