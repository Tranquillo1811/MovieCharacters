using MovieCharacters.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieCharacters.DAL.Models;

namespace MovieCharacters.DAL.Repositories
{
    public class FranchisesRepository : IFranchiseRepository
    {
        public async Task<IFranchise> AddAsync(IFranchise entity)
        {
            IFranchise result;
            using (MovieCharactersContext context = new())
            {
                result = (IFranchise)await context.AddAsync(entity);
                await context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<int> DeleteAsync(IFranchise entity)
        {
            using (MovieCharactersContext context = new ())
            {
                var _franchise = (IFranchise)await context.FindAsync(typeof(IFranchise), entity.Id);
                if (_franchise != null)
                {
                    context.Remove(_franchise);
                    await context.SaveChangesAsync();
                }
            }

            return entity.Id;
        }

        public async Task<IEnumerable<IFranchise>> FindAllAsync(Expression<Func<IFranchise, bool>> predicate)
        {
            List<Franchise> result = new List<Franchise>();

            using (MovieCharactersContext context = new())
            {
                result = await context.Franchises
                    .Include(f => f.Movies).ToListAsync();
            }

            return result;
        }

        public async Task<IEnumerable<IFranchise>> GetAllAsync()
        {
            List<Franchise> result = new List<Franchise>();

            using (MovieCharactersContext context = new())
            {
                result = await context.Franchises
                    .Include(f => f.Movies).ToListAsync();
            }

            return result;
        }

        public async Task<IFranchise> GetByIdAsync(int id)
        {
            IFranchise franchise = new Franchise();

            using (MovieCharactersContext context = new())
            {
                franchise = (IFranchise)await context.FindAsync(typeof(IFranchise), id);
            }

            return franchise;
        }

        public async Task<int> UpdateAsync(IFranchise entity)
        {
            using (MovieCharactersContext context = new())
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }

            return entity.Id;
        }
    }
}
