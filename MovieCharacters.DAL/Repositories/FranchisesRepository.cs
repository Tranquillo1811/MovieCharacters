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
        public bool Add(IFranchise entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(IFranchise entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IFranchise> FindAll(Expression<Func<IFranchise, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IFranchise> GetAll()
        {
            throw new NotImplementedException();
        }

        public IFranchise GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(IFranchise entity)
        {
            throw new NotImplementedException();
        }
    }
}
