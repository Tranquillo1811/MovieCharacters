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
        public Task<IFranchise> AddAsync(IFranchise entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(IFranchise entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IFranchise>> FindAllAsync(Expression<Func<IFranchise, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IFranchise>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public IFranchise GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(IFranchise entity)
        {
            throw new NotImplementedException();
        }
    }
}
