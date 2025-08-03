using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reelr.Migrations
{
    /// <inheritdoc />
    public partial class AddGenresStringToMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteMovies_Movies_MovieId1",
                table: "FavoriteMovies");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteMovies_MovieId1",
                table: "FavoriteMovies");

            migrationBuilder.DropColumn(
                name: "MovieId1",
                table: "FavoriteMovies");

            migrationBuilder.RenameColumn(
                name: "Genres",
                table: "Movies",
                newName: "GenresString");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_TmdbId",
                table: "Movies",
                column: "TmdbId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Movies_TmdbId",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "GenresString",
                table: "Movies",
                newName: "Genres");

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
    }
}
