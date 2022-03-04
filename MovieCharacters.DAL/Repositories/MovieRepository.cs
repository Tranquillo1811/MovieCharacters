using Microsoft.EntityFrameworkCore;
using MovieCharacters.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MovieCharacters.DAL.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieCharactersContext _context;

        public MovieRepository(MovieCharactersContext context)
        {
            _context = context;
        }

        /// <summary>
        /// adds a movie to the Db
        /// </summary>
        /// <param name="entity">Movie to be added to the Db</param>
        /// <returns>newly added movie</returns>
        public async Task<Movie> AddAsync(Movie entity)
        {
            Movie movieResult = null;
            try
            {
                movieResult = (await _context.Movies.AddAsync(entity)).Entity;
                int intResult = await _context.SaveChangesAsync();
            }
            catch
            {
                //TODO: not quite sure, how to actually handle this...
            }
            return movieResult;
        }

        /// <summary>
        /// deletes Movie with respective Id
        /// </summary>
        /// <param name="MovieId">id of the movie to be deleted</param>
        /// <returns>id of the deleted movie</returns>
        public async Task<int> DeleteByIdAsync(int MovieId)
        {
            Movie deleteMovie = await GetByIdAsync(MovieId);
            try
            {
                _context.Movies.Remove(deleteMovie);
                await _context.SaveChangesAsync();
            }
            catch
            {
                //TODO: not quite sure, how to actually handle this...
            }
            return MovieId;
        }

        /// <summary>
        /// gets all movies from Db
        /// </summary>
        /// <returns>List of all movies from Movies Db table</returns>
        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            List<Movie> movies = new();
            try
            {
                movies = await _context.Movies.Include(m => m.Characters).ToListAsync();
            }
            catch
            {
                //TODO: not quite sure, how to actually handle this...
            }
            return movies;
        }

        /// <summary>
        /// get movie with particular Id from Movies table
        /// </summary>
        /// <param name="id">id of the movie to be selected</param>
        /// <returns>movie with given id or null if no such movie exists</returns>
        public async Task<Movie> GetByIdAsync(int id)
        {
            Movie movie = null;
            try
            {
                movie = await _context.Movies.Include(m => m.Characters).FirstOrDefaultAsync(m => m.Id == id);
            }
            catch
            {
                //TODO: not quite sure, how to actually handle this...
            }
            return movie;
        }

        /// <summary>
        /// updates a movie in Movies Db table
        /// </summary>
        /// <param name="entity">movie to be updated</param>
        /// <returns>updated movie</returns>
        public async Task<Movie> UpdateAsync(Movie entity)
        {
            Movie movieResult;
            _context.Entry(await _context.Movies.FindAsync(entity.Id)).State = EntityState.Detached;
            _context.Entry(entity).State = EntityState.Modified;
            int intResult = 0;
            try
            {
                intResult = await _context.SaveChangesAsync();
            }
            catch
            {
                //TODO: not quite sure, how to actually handle this...
            }
            if (intResult == 0)
                return null;
            movieResult = entity;
            return movieResult;
        }

        /// <summary>
        /// assigns characters to a movie, all previously existent characters will be removed from
        /// that movie
        /// </summary>
        /// <param name="Movie">movie to set characters for</param>
        /// <param name="CharacterIds">ids of all characters that should be assigned to this movie</param>
        /// <returns>Movie with changed characters</returns>
        public async Task<Movie> SetCharacterIdsAsync(Movie Movie, int[] CharacterIds)
        {
            Movie = _context.Movies.Include(m => m.Characters).FirstOrDefault(m => m.Id == Movie.Id);
            //--- add all characters to movie which are not in there already
            foreach (int characterId in CharacterIds)
            {
                if (!Movie.Characters.Any(c => c.Id == characterId))
                    Movie.Characters.Add(await _context.Characters.FindAsync(characterId));
            }
            //--- remove all characters from movie which are not supposed to be there any longer
            foreach (Character character in Movie.Characters)
            {
                if (!CharacterIds.Contains(character.Id))
                    Movie.Characters.Remove(character);
            }
            int intResult = 0;
            try
            {
                intResult = await _context.SaveChangesAsync();
            }
            catch
            {
                //TODO: not quite sure, how to actually handle this...
            }
            if (intResult == 0)
                return null;
            return Movie;
        }

        /// <summary>
        /// gets all characters of a particular movie
        /// </summary>
        /// <param name="MovieId">id of the movie to get characters from</param>
        /// <returns>all characters of the movie with this Id</returns>
        public async Task<IEnumerable<Character>> GetCharactersById(int MovieId)
        {
            List<Character> characters = null;
            try
            {
                Movie movie = await _context.Movies.Include(m => m.Characters).FirstOrDefaultAsync(m => m.Id == MovieId);
                if (movie != null)
                    characters = movie.Characters.ToList();
            }
            catch
            {
                //TODO: not quite sure, how to actually handle this...
            }
            return characters;
        }
    }
}
