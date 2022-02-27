using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieCharacters.DAL.Migrations
{
    public partial class SeedSomeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacterMovie",
                table: "CharacterMovie");

            migrationBuilder.DropIndex(
                name: "IX_CharacterMovie_MoviesId",
                table: "CharacterMovie");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacterMovie",
                table: "CharacterMovie",
                columns: new[] { "MoviesId", "CharactersId" });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Alias", "FullName", "Gender", "PictureUrl" },
                values: new object[,]
                {
                    { 1, null, "Elijah Wood", "Male", "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c9/Elijah_Wood_%2847955397556%29_%28cropped%29.jpg/220px-Elijah_Wood_%2847955397556%29_%28cropped%29.jpg" },
                    { 2, null, "Liv Tyler", "Female", "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2f/Liv_Tyler_%2829566238128%29_%28cropped%29.jpg/220px-Liv_Tyler_%2829566238128%29_%28cropped%29.jpg" },
                    { 3, "Viggo Mortensen", "Viggo Peter Mortensen Jr.", "Male", "https://upload.wikimedia.org/wikipedia/commons/thumb/6/64/Viggo_Mortensen_B_%282020%29.jpg/220px-Viggo_Mortensen_B_%282020%29.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Franchises",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "The Lord of the Rings is a series of three epic fantasy adventure films directed by Peter Jackson, based on the novel written by J. R. R. Tolkien.", "Lord of the Rings" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Director", "FranchiseId", "Genres", "MoviePosterUrl", "ReleaseYear", "Title", "TrailerUrl" },
                values: new object[] { 1, "Peter Jackson", 1, "Fantasy", "https://upload.wikimedia.org/wikipedia/en/8/8a/The_Lord_of_the_Rings_The_Fellowship_of_the_Ring_%282001%29.jpg", 2001, "The Fellowship of the Ring", "https://www.youtube.com/watch?v=V75dMMIW2B4" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Director", "FranchiseId", "Genres", "MoviePosterUrl", "ReleaseYear", "Title", "TrailerUrl" },
                values: new object[] { 2, "Peter Jackson", 1, "Fantasy", "https://upload.wikimedia.org/wikipedia/en/d/d0/Lord_of_the_Rings_-_The_Two_Towers_%282002%29.jpg", 2002, "The Two Towers", "https://www.youtube.com/watch?v=LbfMDwc4azU" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Director", "FranchiseId", "Genres", "MoviePosterUrl", "ReleaseYear", "Title", "TrailerUrl" },
                values: new object[] { 3, "Peter Jackson", 1, "Fantasy", "https://upload.wikimedia.org/wikipedia/en/b/be/The_Lord_of_the_Rings_-_The_Return_of_the_King_%282003%29.jpg", 2003, "The Return of the King", "https://www.youtube.com/watch?v=r5X-hFf6Bwo" });

            migrationBuilder.InsertData(
                table: "CharacterMovie",
                columns: new[] { "CharactersId", "MoviesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 2 },
                    { 1, 3 },
                    { 2, 3 },
                    { 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMovie_CharactersId",
                table: "CharacterMovie",
                column: "CharactersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacterMovie",
                table: "CharacterMovie");

            migrationBuilder.DropIndex(
                name: "IX_CharacterMovie_CharactersId",
                table: "CharacterMovie");

            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumns: new[] { "CharactersId", "MoviesId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumns: new[] { "CharactersId", "MoviesId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumns: new[] { "CharactersId", "MoviesId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumns: new[] { "CharactersId", "MoviesId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumns: new[] { "CharactersId", "MoviesId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumns: new[] { "CharactersId", "MoviesId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumns: new[] { "CharactersId", "MoviesId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumns: new[] { "CharactersId", "MoviesId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumns: new[] { "CharactersId", "MoviesId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Franchises",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacterMovie",
                table: "CharacterMovie",
                columns: new[] { "CharactersId", "MoviesId" });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMovie_MoviesId",
                table: "CharacterMovie",
                column: "MoviesId");
        }
    }
}
