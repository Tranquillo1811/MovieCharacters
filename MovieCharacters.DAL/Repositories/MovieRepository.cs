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
        public async Task<Movie> AddAsync(Movie entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteByIdAsync(int entityId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> FindAllAsync(Expression<Func<Movie, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Movie> UpdateAsync(Movie entity)
        {
            throw new NotImplementedException();
        }
    }
}
