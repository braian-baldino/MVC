using Microsoft.EntityFrameworkCore.Migrations;

namespace Accountant.MVC.Migrations
{
    public partial class MonthPropertyList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "Balances");

            migrationBuilder.AddColumn<int>(
                name: "EMonth",
                table: "Balances",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Months",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Months", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balances_Months_EMonth",
                table: "Balances");

            migrationBuilder.DropTable(
                name: "Months");

            migrationBuilder.DropIndex(
                name: "IX_Balances_EMonth",
                table: "Balances");

            migrationBuilder.DropColumn(
                name: "EMonth",
                table: "Balances");

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "Balances",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
