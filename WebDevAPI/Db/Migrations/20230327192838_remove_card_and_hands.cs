﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevAPI.Migrations
{
    /// <inheritdoc />
    public partial class remove_card_and_hands : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "PlayerHands");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerHands",
                columns: table => new
                {
                    PlayerHandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerHands", x => x.PlayerHandId);
                    table.ForeignKey(
                        name: "FK_PlayerHands_Users_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    CardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerHandId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PokerTableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MySuit = table.Column<int>(type: "int", nullable: false),
                    MyValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_Cards_PlayerHands_PlayerHandId",
                        column: x => x.PlayerHandId,
                        principalTable: "PlayerHands",
                        principalColumn: "PlayerHandId");
                    table.ForeignKey(
                        name: "FK_Cards_PokerTables_PokerTableId",
                        column: x => x.PokerTableId,
                        principalTable: "PokerTables",
                        principalColumn: "PokerTableId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_PlayerHandId",
                table: "Cards",
                column: "PlayerHandId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_PokerTableId",
                table: "Cards",
                column: "PokerTableId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerHands_PlayerId",
                table: "PlayerHands",
                column: "PlayerId",
                unique: true);
        }
    }
}
