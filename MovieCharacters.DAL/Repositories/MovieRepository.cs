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
        /// <summary>
        /// adds a movie to the Db
        /// </summary>
        /// <param name="entity">Movie to be added to the Db</param>
        /// <returns>newly added movie</returns>
        public async Task<Movie> AddAsync(Movie entity)
        {
            Movie movieResult = null;
            using (MovieCharactersContext context = new())
            {
                try
                {
                    movieResult = (await context.Movies.AddAsync(entity)).Entity;
                    int intResult = await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    //TODO: not quite sure, how to actually handle this...
                }
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
            using (MovieCharactersContext context = new())
            {
                try
                {
                    context.Movies.Remove(deleteMovie);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    //TODO: not quite sure, how to actually handle this...
                }
            }
            return MovieId;
        }

        public async Task<IEnumerable<Movie>> FindAllAsync(Expression<Func<Movie, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// gets all movies from Db
        /// </summary>
        /// <returns>List of all movies from Movies Db table</returns>
        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            List<Movie> movies = new();
            using (MovieCharactersContext context = new())
            {
                try
                {
                    movies = await context.Movies.Include(m => m.Characters).ToListAsync();
                }
                catch (Exception ex)
                {
                    //TODO: not quite sure, how to actually handle this...
                }
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
            using (MovieCharactersContext context = new())
            {
                try
                {
                    movie = await context.Movies.Include(m => m.Characters).FirstOrDefaultAsync(m => m.Id == id);
                }
                catch (Exception ex)
                {
                    //TODO: not quite sure, how to actually handle this...
                }
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
            using (MovieCharactersContext context = new())
            {
                context.Entry(entity).State = EntityState.Modified;
                int intResult = 0;
                try
                {
                    intResult = await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    //TODO: not quite sure, how to actually handle this...
                }
                if (intResult == 0)
                    return null;
                movieResult = entity;
            }
            return movieResult;
        }

        public async Task<Movie> SetCharacterIdsAsync(Movie movie, int[] characterIds)
        {
            using (MovieCharactersContext context = new())
            {
                //context.Entry(movie).State = EntityState.Modified;
                movie = context.Movies.Include(m => m.Characters).FirstOrDefault(m => m.Id == movie.Id);
                //--- add all characters to movie which are not in there already
                foreach (int characterId in characterIds)
                {
                    if (!movie.Characters.Any(c => c.Id == characterId))
                        movie.Characters.Add(await context.Characters.FindAsync(characterId));
                }
                //--- remove all characters from movie which are not supposed to be there any longer
                foreach(Character character in movie.Characters)
                {
                    if (!characterIds.Contains(character.Id))
                        movie.Characters.Remove(character);
                }
                int intResult = 0;
                try
                {
                    intResult = await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    //TODO: not quite sure, how to actually handle this...
                }
                if (intResult == 0)
                    return null;
            }
            return movie;
        }
    }
}
