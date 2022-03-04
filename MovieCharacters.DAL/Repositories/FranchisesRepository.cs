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
        /// <summary>
        /// adds a franchise to the Db
        /// </summary>
        /// <param name="entity">Franchise to be added to the Db</param>
        /// <returns>newly added franchise</returns>
        public async Task<Franchise> AddAsync(Franchise entity)
        {
            Franchise franchiseResult = null;
            using (MovieCharactersContext context = new())
            {
                try
                {
                    franchiseResult = (await context.Franchises.AddAsync(entity)).Entity;
                    int intResult = await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    //TODO: not quite sure, how to actually handle this...
                }
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
            using (MovieCharactersContext context = new())
            {
                try
                {
                    context.Franchises.Remove(deleteFranchise);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    //TODO: not quite sure, how to actually handle this...
                }
            }
            return entityId;
        }

        public async Task<IEnumerable<Franchise>> FindAllAsync(Expression<Func<Franchise, bool>> predicate)
        {
            List<Franchise> result = new List<Franchise>();

            using (MovieCharactersContext context = new())
            {
                result = await context.Franchises
                    .Include(f => f.Movies).ToListAsync();
            }

            return result;
        }

        /// <summary>
        /// gets all franchises from Db
        /// </summary>
        /// <returns>List of all franchises from Franchises Db table</returns>
        public async Task<IEnumerable<Franchise>> GetAllAsync()
        {
            List<Franchise> franchises = new();
            using (MovieCharactersContext context = new())
            {
                try
                {
                    franchises = await context.Franchises.Include(f => f.Movies).ToListAsync();
                }
                catch (Exception ex)
                {
                    //TODO: not quite sure, how to actually handle this...
                }
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
            using (MovieCharactersContext context = new())
            {
                try
                {
                    franchise = await context.Franchises
                            .Include(f => f.Movies)
                            .FirstOrDefaultAsync(f => f.Id == id);
                }
                catch (Exception ex)
                {
                    //TODO: not quite sure, how to actually handle this...
                }
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
            using (MovieCharactersContext context = new())
            {
                //context.Entry(movie).State = EntityState.Modified;
                Franchise = context.Franchises.Include(f => f.Movies).FirstOrDefault(f => f.Id == Franchise.Id);
                //--- add all characters to movie which are not in there already
                foreach (int movieId in MovieIds)
                {
                    if (!Franchise.Movies.Any(c => c.Id == movieId))
                        Franchise.Movies.Add(await context.Movies.FindAsync(movieId));
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
                    intResult = await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    //TODO: not quite sure, how to actually handle this...
                }
                if (intResult == 0)
                    return null;
            }
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
                franchiseResult = entity;
            }
            return franchiseResult;
        }
    }
}
