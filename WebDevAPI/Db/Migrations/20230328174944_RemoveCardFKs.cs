using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCardFKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_PlayerHands_PlayerHandId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_PlayerHandId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "PlayerHandId",
                table: "Cards");

            migrationBuilder.AddColumn<Guid>(
                name: "FirstCardId",
                table: "PlayerHands",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SecondCardId",
                table: "PlayerHands",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerHands_FirstCardId",
                table: "PlayerHands",
                column: "FirstCardId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerHands_SecondCardId",
                table: "PlayerHands",
                column: "SecondCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerHands_Cards_FirstCardId",
                table: "PlayerHands",
                column: "FirstCardId",
                principalTable: "Cards",
                principalColumn: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerHands_Cards_SecondCardId",
                table: "PlayerHands",
                column: "SecondCardId",
                principalTable: "Cards",
                principalColumn: "CardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerHands_Cards_FirstCardId",
                table: "PlayerHands");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerHands_Cards_SecondCardId",
                table: "PlayerHands");

            migrationBuilder.DropIndex(
                name: "IX_PlayerHands_FirstCardId",
                table: "PlayerHands");

            migrationBuilder.DropIndex(
                name: "IX_PlayerHands_SecondCardId",
                table: "PlayerHands");

            migrationBuilder.DropColumn(
                name: "FirstCardId",
                table: "PlayerHands");

            migrationBuilder.DropColumn(
                name: "SecondCardId",
                table: "PlayerHands");

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerHandId",
                table: "Cards",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Cards_PlayerHandId",
                table: "Cards",
                column: "PlayerHandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_PlayerHands_PlayerHandId",
                table: "Cards",
                column: "PlayerHandId",
                principalTable: "PlayerHands",
                principalColumn: "PlayerHandId");
        }
    }
}
