using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevAPI.Migrations
{
    /// <inheritdoc />
    public partial class removed_my_userTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerHands_MyUsers_PlayerId",
                table: "PlayerHands");

            migrationBuilder.DropTable(
                name: "MyUsers");

            migrationBuilder.AlterColumn<string>(
                name: "PlayerId",
                table: "PlayerHands",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "AuthCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Chips",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PokerTableId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PokerTableId",
                table: "AspNetUsers",
                column: "PokerTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PokerTables_PokerTableId",
                table: "AspNetUsers",
                column: "PokerTableId",
                principalTable: "PokerTables",
                principalColumn: "PokerTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerHands_AspNetUsers_PlayerId",
                table: "PlayerHands",
                column: "PlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PokerTables_PokerTableId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerHands_AspNetUsers_PlayerId",
                table: "PlayerHands");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PokerTableId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AuthCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Chips",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PokerTableId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlayerId",
                table: "PlayerHands",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "MyUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PokerTableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Chips = table.Column<int>(type: "int", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyUsers_PokerTables_PokerTableId",
                        column: x => x.PokerTableId,
                        principalTable: "PokerTables",
                        principalColumn: "PokerTableId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyUsers_PokerTableId",
                table: "MyUsers",
                column: "PokerTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerHands_MyUsers_PlayerId",
                table: "PlayerHands",
                column: "PlayerId",
                principalTable: "MyUsers",
                principalColumn: "Id");
        }
    }
}
