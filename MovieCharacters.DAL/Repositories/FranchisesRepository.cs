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
                franchise = (Franchise)await context.FindAsync(typeof(Franchise), id);
            }

            return franchise;
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
