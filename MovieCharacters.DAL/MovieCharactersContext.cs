using Microsoft.EntityFrameworkCore;
using MovieCharacters.DAL.Models;
using System.Collections.Generic;

namespace MovieCharacters.DAL
{
    internal class MovieCharactersContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
        public DbSet<Character> Characters { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Data Source = localhost; Initial Catalog = MovieCharactersDb; Integrated Security = True; Trust Server Certificate = True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<Character> seedCharacters = new()
            {
                new Character()
                {
                    Id = 1,
                    FullName = "Elijah Wood",
                    Gender = "Male",
                    PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c9/Elijah_Wood_%2847955397556%29_%28cropped%29.jpg/220px-Elijah_Wood_%2847955397556%29_%28cropped%29.jpg"
                },
                new Character()
                {
                    Id = 2,
                    FullName = "Liv Tyler",
                    Gender = "Female",
                    PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2f/Liv_Tyler_%2829566238128%29_%28cropped%29.jpg/220px-Liv_Tyler_%2829566238128%29_%28cropped%29.jpg"
                },
                new Character()
                {
                    Id = 3,
                    FullName = "Viggo Peter Mortensen Jr.",
                    Alias = "Viggo Mortensen",
                    Gender = "Male",
                    PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/64/Viggo_Mortensen_B_%282020%29.jpg/220px-Viggo_Mortensen_B_%282020%29.jpg"
                }
            };
            List<Movie> seedMovies = new()
            {
                new Movie()
                {
                    Id = 1,
                    Director = "Peter Jackson",
                    Genres = "Fantasy",
                    ReleaseYear = 2001,
                    Title = "The Fellowship of the Ring",
                    MoviePosterUrl = "https://upload.wikimedia.org/wikipedia/en/8/8a/The_Lord_of_the_Rings_The_Fellowship_of_the_Ring_%282001%29.jpg",
                    TrailerUrl = "https://www.youtube.com/watch?v=V75dMMIW2B4",
                    FranchiseId = 1
                },
                new Movie()
                {
                    Id = 2,
                    Director = "Peter Jackson",
                    Genres = "Fantasy",
                    ReleaseYear = 2002,
                    Title = "The Two Towers",
                    MoviePosterUrl = "https://upload.wikimedia.org/wikipedia/en/d/d0/Lord_of_the_Rings_-_The_Two_Towers_%282002%29.jpg",
                    TrailerUrl = "https://www.youtube.com/watch?v=LbfMDwc4azU",
                    FranchiseId = 1
                },
                new Movie()
                {
                    Id = 3,
                    Director = "Peter Jackson",
                    Genres = "Fantasy",
                    ReleaseYear = 2003,
                    Title = "The Return of the King",
                    MoviePosterUrl = "https://upload.wikimedia.org/wikipedia/en/b/be/The_Lord_of_the_Rings_-_The_Return_of_the_King_%282003%29.jpg",
                    TrailerUrl = "https://www.youtube.com/watch?v=r5X-hFf6Bwo",
                    FranchiseId = 1
                }
            };
            List<Franchise> seedFranchises = new()
            {
                new Franchise()
                {
                    Id = 1,
                    Name = "Lord of the Rings",
                    Description = "The Lord of the Rings is a series of three epic fantasy adventure films directed by Peter Jackson, based on the novel written by J. R. R. Tolkien."
                }
            };

            modelBuilder.Entity<Character>()
                .HasData(
                    seedCharacters
                );

            modelBuilder.Entity<Franchise>()
                .HasData(
                    seedFranchises
                );

            modelBuilder.Entity<Movie>()
                .HasData(
                   seedMovies
                );

            modelBuilder.Entity<Movie>()
                .HasMany(movie => movie.Characters)
                .WithMany(characters => characters.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "CharacterMovie",
                    r => r.HasOne<Character>().WithMany().HasForeignKey("CharactersId"),
                    l => l.HasOne<Movie>().WithMany().HasForeignKey("MoviesId"),
                    je =>
                    {
                        je.HasKey("MoviesId", "CharactersId");
                        je.HasData(
                            new { MoviesId = 1, CharactersId = 1 },
                            new { MoviesId = 2, CharactersId = 1 },
                            new { MoviesId = 3, CharactersId = 1 },
                            new { MoviesId = 1, CharactersId = 2 },
                            new { MoviesId = 2, CharactersId = 2 },
                            new { MoviesId = 3, CharactersId = 2 },
                            new { MoviesId = 1, CharactersId = 3 },
                            new { MoviesId = 2, CharactersId = 3 },
                            new { MoviesId = 3, CharactersId = 3 });
                    });
        }
    }
}
