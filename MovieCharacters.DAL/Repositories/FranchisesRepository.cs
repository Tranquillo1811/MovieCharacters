using MovieCharacters.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieCharacters.DAL.Repositories
{
    public class FranchisesRepository : IFranchiseRepository
    {
        private readonly MovieCharactersContext _context;

        public FranchisesRepository(MovieCharactersContext context)
        {
            _context = context;
        }

        /// <summary>
        /// adds a franchise to the Db
        /// </summary>
        /// <param name="entity">Franchise to be added to the Db</param>
        /// <returns>newly added franchise</returns>
        public async Task<Franchise> AddAsync(Franchise entity)
        {
            Franchise franchiseResult = null;
            try
            {
                franchiseResult = (await _context.Franchises.AddAsync(entity)).Entity;
                int intResult = await _context.SaveChangesAsync();
            }
            catch
            {
                //TODO: not quite sure, how to actually handle this...
            }
            return franchiseResult;
        }

        /// <summary>
        /// deletes franchise with respective Id
        /// </summary>
        /// <param name="CharacterId">id of the franchise to be deleted</param>
        /// <returns>id of the deleted fracnhise</returns>
        public async Task<int> DeleteByIdAsync(int entityId)
        {
            Franchise deleteFranchise = await GetByIdAsync(entityId);
            try
            {
                _context.Franchises.Remove(deleteFranchise);
                await _context.SaveChangesAsync();
            }
            catch
            {
                //TODO: not quite sure, how to actually handle this...
            }
            return entityId;
        }

        public async Task<IEnumerable<Franchise>> FindAllAsync(Expression<Func<Franchise, bool>> predicate)
        {
            List<Franchise> result = new ();
            result = await _context.Franchises.Include(f => f.Movies).ToListAsync();

            return result;
        }

        /// <summary>
        /// gets all franchises from Db
        /// </summary>
        /// <returns>List of all franchises from Franchises Db table</returns>
        public async Task<IEnumerable<Franchise>> GetAllAsync()
        {
            List<Franchise> franchises = new();
            try
            {
                franchises = await _context.Franchises.Include(f => f.Movies).ToListAsync();
            }
            catch
            {
                //TODO: not quite sure, how to actually handle this...
            }
            return franchises;
        }

        /// <summary>
        /// get franchise with particular Id from Franchises table
        /// </summary>
        /// <param name="id">id of the franchise to be selected</param>
        /// <returns>franchise with given id or null if no such franchise exists</returns>
        public async Task<Franchise> GetByIdAsync(int id)
        {
            Franchise franchise = null;
            try
            {
                franchise = await _context.Franchises
                        .Include(f => f.Movies)
                        .FirstOrDefaultAsync(f => f.Id == id);
            }
            catch
            {
                //TODO: not quite sure, how to actually handle this...
            }
            return franchise;
        }

        /// <summary>
        /// assigns movies to a franchise, all previously existent movies will be removed from
        /// that franchise
        /// </summary>
        /// <param name="Franchise">franchise to set movies for</param>
        /// <param name="MovieIds">ids of all movies that should be assigned to this franchise</param>
        /// <returns></returns>
        public async Task<Franchise> SetMovieIdsAsync(Franchise Franchise, int[] MovieIds)
        {
            Franchise = _context.Franchises.Include(f => f.Movies).FirstOrDefault(f => f.Id == Franchise.Id);
            //--- add all characters to movie which are not in there already
            foreach (int movieId in MovieIds)
            {
                if (!Franchise.Movies.Any(c => c.Id == movieId))
                    Franchise.Movies.Add(await _context.Movies.FindAsync(movieId));
            }
            //--- remove all characters from movie which are not supposed to be there any longer
            foreach (Movie movie in Franchise.Movies)
            {
                if (!MovieIds.Contains(movie.Id))
                    Franchise.Movies.Remove(movie);
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
            return Franchise;
        }

        /// <summary>
        /// updates a franchise in Franchises Db table
        /// </summary>
        /// <param name="entity">franchise to be updated</param>
        /// <returns>updated franchise</returns>
        public async Task<Franchise> UpdateAsync(Franchise entity)
        {
            Franchise franchiseResult;
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
            franchiseResult = entity;
            return franchiseResult;
        }

        /// <summary>
        /// gets all movies of a particular franchise
        /// </summary>
        /// <param name="FranchiseId">Id of the franchise to get movies from</param>
        /// <returns>movies of the franchise with that Id or null if no franchise exists with that Id</returns>
        public async Task<IEnumerable<Movie>> GetMoviesById(int FranchiseId)
        {
            List<Movie> movies = null;
            try
            {
                Franchise franchise = await _context.Franchises.Include(f => f.Movies).FirstOrDefaultAsync(f => f.Id == FranchiseId);
                if (franchise != null)
                    movies = (await _context.Franchises.Include(f => f.Movies).FirstOrDefaultAsync(f => f.Id == FranchiseId)).Movies?.ToList();
            }
            catch
            {
                //TODO: not quite sure, how to actually handle this...
            }
            return movies;
        }

        /// <summary>
        /// gets all characters of a particular franchise
        /// </summary>
        /// <param name="FranchiseId">Id of the franchise to get characters from</param>
        /// <returns>characters of the franchise with that Id or null if no franchise exists with that Id</returns>
        public async Task<IEnumerable<Character>> GetCharactersById(int FranchiseId)
        {
            List<Character> characters = null;
            try
            {
                Franchise franchise = await _context.Franchises.Include(f => f.Movies).ThenInclude(m => m.Characters).FirstOrDefaultAsync(f => f.Id == FranchiseId);
                if (franchise != null)
                    characters = franchise.Movies?.SelectMany(m => m.Characters).Distinct().ToList();
            }
            catch
            {
                //TODO: not quite sure, how to actually handle this...
            }
            return characters;
        }
    }
}
