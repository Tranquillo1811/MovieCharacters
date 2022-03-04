using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCharacters.BLL.Models
{
    public interface IFranchiseRepository : IRepository<Franchise>
    {
        public Task<Franchise> SetMovieIdsAsync(Franchise Franchise, int[] MovieIds);

        public Task<IEnumerable<Movie>> GetMoviesById(int FranchiseId);

        public Task<IEnumerable<Character>> GetCharactersById(int FranchiseId);
    }
}
