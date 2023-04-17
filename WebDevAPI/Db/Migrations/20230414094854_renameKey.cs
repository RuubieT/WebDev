using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevAPI.Migrations
{
    /// <inheritdoc />
    public partial class renameKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Users",
                newName: "AuthCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthCode",
                table: "Users",
                newName: "Key");
        }
    }
}
