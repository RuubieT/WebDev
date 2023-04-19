using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevAPI.Migrations
{
    /// <inheritdoc />
    public partial class renameMyUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerHands_Users_PlayerId",
                table: "PlayerHands");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_PokerTables_PokerTableId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "MyUsers");

            migrationBuilder.RenameIndex(
                name: "IX_Users_PokerTableId",
                table: "MyUsers",
                newName: "IX_MyUsers_PokerTableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyUsers",
                table: "MyUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MyUsers_PokerTables_PokerTableId",
                table: "MyUsers",
                column: "PokerTableId",
                principalTable: "PokerTables",
                principalColumn: "PokerTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerHands_MyUsers_PlayerId",
                table: "PlayerHands",
                column: "PlayerId",
                principalTable: "MyUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyUsers_PokerTables_PokerTableId",
                table: "MyUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerHands_MyUsers_PlayerId",
                table: "PlayerHands");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MyUsers",
                table: "MyUsers");

            migrationBuilder.RenameTable(
                name: "MyUsers",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_MyUsers_PokerTableId",
                table: "Users",
                newName: "IX_Users_PokerTableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerHands_Users_PlayerId",
                table: "PlayerHands",
                column: "PlayerId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PokerTables_PokerTableId",
                table: "Users",
                column: "PokerTableId",
                principalTable: "PokerTables",
                principalColumn: "PokerTableId");
        }
    }
}
