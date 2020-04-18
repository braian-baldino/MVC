﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Accountant.MVC.Migrations
{
    public partial class fix3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balances_Months_MonthId",
                table: "Balances");

            migrationBuilder.DropTable(
                name: "Months");

            migrationBuilder.DropIndex(
                name: "IX_Balances_MonthId",
                table: "Balances");

            migrationBuilder.DropColumn(
                name: "MonthId",
                table: "Balances");

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "Balances",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "Balances");

            migrationBuilder.AddColumn<int>(
                name: "MonthId",
                table: "Balances",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Months",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Months", x => x.Id);
                });

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
    }
}
