using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FootballBetting.Data.Migrations
{
    public partial class BetUserAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Bets_UserId",
                table: "Bets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bets_Users_UserId",
                table: "Bets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bets_Users_UserId",
                table: "Bets");

            migrationBuilder.DropIndex(
                name: "IX_Bets_UserId",
                table: "Bets");
        }
    }
}
