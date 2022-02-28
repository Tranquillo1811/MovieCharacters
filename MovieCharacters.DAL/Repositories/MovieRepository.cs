using MovieCharacters.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieCharacters.DAL.Repositories
{
    public class MovieRepository : MovieCharacters.BLL.Models.IMovieRepository
    {
        public bool Add(IMovie entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(IMovie entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMovie> FindAll(Expression<Func<IMovie, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMovie> GetAll()
        {
            throw new NotImplementedException();
        }

        public IMovie GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(IMovie entity)
        {
            throw new NotImplementedException();
        }
    }
}
