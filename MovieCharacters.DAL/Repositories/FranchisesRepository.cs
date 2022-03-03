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
        public async Task<Franchise> AddAsync(Franchise entity)
        {
            using (MovieCharactersContext context = new())
            {
                var franchise = await context.AddAsync(entity);
                await context.SaveChangesAsync();
            }
            return entity;
        }

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

        public async Task<IEnumerable<Franchise>> GetAllAsync()
        {
            List<Franchise> result = new List<Franchise>();

            using (MovieCharactersContext context = new())
            {
                result = await context.Franchises
                    .Include(f => f.Movies).ToListAsync();
            }

            return result;
        }

        public async Task<Franchise> GetByIdAsync(int id)
        {
            Franchise franchise = new Franchise();

            using (MovieCharactersContext context = new())
            {
                //franchise = (Franchise)await context.FindAsync(typeof(Franchise), id);
                franchise = await context.Franchises.Include(f => f.Movies).FirstOrDefaultAsync(f => f.Id == id);
            }

            return franchise;
        }

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

        public async Task<int> UpdateAsync(Franchise entity)
        {
            using (MovieCharactersContext context = new())
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }

            return entity.Id;
        }

        Task<Franchise> IRepository<Franchise>.UpdateAsync(Franchise entity)
        {
            throw new NotImplementedException();
        }
    }
}
