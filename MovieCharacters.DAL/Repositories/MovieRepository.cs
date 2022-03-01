using MovieCharacters.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieCharacters.DAL.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        public Task<IMovie> AddAsync(IMovie entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(IMovie entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IMovie>> FindAllAsync(Expression<Func<IMovie, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IMovie>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IMovie> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(IMovie entity)
        {
            throw new NotImplementedException();
        }
    }
}
