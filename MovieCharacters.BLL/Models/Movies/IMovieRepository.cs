using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCharacters.BLL.Models
{
    public interface IMovieRepository : IRepository<Movie>
    {
        public Task<Movie> SetCharacterIdsAsync(Movie movie, int[] characterIds);

        public Task<IEnumerable<Character>> GetCharactersById(int FranchiseId);
    }
}
