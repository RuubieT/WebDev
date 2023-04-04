using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevAPI.Migrations
{
    /// <inheritdoc />
    public partial class CardsInHandAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InHand",
                table: "Cards",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InHand",
                table: "Cards");
        }
    }
}
