using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RamazonBot.Dal.Migrations
{
    /// <inheritdoc />
    public partial class Test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TelegramBots_UserId",
                table: "TelegramBots");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TelegramBots_UserId",
                table: "TelegramBots",
                column: "UserId",
                unique: true);
        }
    }
}
