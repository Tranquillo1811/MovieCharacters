using MovieCharacters.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieCharacters.DAL.Repositories
{
    public class FranchisesRepository : IFranchiseRepository
    {
        public Task<Franchise> AddAsync(Franchise entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Franchise entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Franchise>> FindAllAsync(Expression<Func<Franchise, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Franchise>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Franchise> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Franchise> UpdateAsync(Franchise entity)
        {
            throw new NotImplementedException();
        }
    }
}
