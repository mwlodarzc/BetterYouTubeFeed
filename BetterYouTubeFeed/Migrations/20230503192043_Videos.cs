using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetterYouTubeFeed.Migrations
{
    /// <inheritdoc />
    public partial class Videos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Link",
                table: "Videos",
                newName: "VideoId");

            migrationBuilder.RenameColumn(
                name: "Link",
                table: "Channels",
                newName: "CustomUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VideoId",
                table: "Videos",
                newName: "Link");

            migrationBuilder.RenameColumn(
                name: "CustomUrl",
                table: "Channels",
                newName: "Link");
        }
    }
}
