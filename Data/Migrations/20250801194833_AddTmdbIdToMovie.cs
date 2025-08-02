using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reelr.Migrations
{
    /// <inheritdoc />
    public partial class AddTmdbIdToMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TmdbId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovieId1",
                table: "FavoriteMovies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMovies_MovieId1",
                table: "FavoriteMovies",
                column: "MovieId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteMovies_Movies_MovieId1",
                table: "FavoriteMovies",
                column: "MovieId1",
                principalTable: "Movies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteMovies_Movies_MovieId1",
                table: "FavoriteMovies");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteMovies_MovieId1",
                table: "FavoriteMovies");

            migrationBuilder.DropColumn(
                name: "TmdbId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MovieId1",
                table: "FavoriteMovies");
        }
    }
}
