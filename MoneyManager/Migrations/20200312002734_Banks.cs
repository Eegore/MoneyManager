using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MoneyManager.Migrations
{
    public partial class Banks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deposits_Bank_BankId",
                table: "Deposits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bank",
                table: "Bank");

            migrationBuilder.RenameTable(
                name: "Bank",
                newName: "Banks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Banks",
                table: "Banks",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deposits_Banks_BankId",
                table: "Deposits",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "BankId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deposits_Banks_BankId",
                table: "Deposits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Banks",
                table: "Banks");

            migrationBuilder.RenameTable(
                name: "Banks",
                newName: "Bank");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bank",
                table: "Bank",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deposits_Bank_BankId",
                table: "Deposits",
                column: "BankId",
                principalTable: "Bank",
                principalColumn: "BankId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
