using Microsoft.EntityFrameworkCore;
using MovieCharacters.BLL.Models;

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
    }
}
