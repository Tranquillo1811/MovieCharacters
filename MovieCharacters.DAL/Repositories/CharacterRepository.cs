using Microsoft.EntityFrameworkCore;
using MovieCharacters.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieCharacters.DAL.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        public Task<int> AddAsync(ICharacter entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(ICharacter entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ICharacter>> FindAllAsync(Expression<Func<ICharacter, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ICharacter>> GetAllAsync()
        {
            List<ICharacter> characters = new ();
            using(MovieCharactersContext context = new ())
            {
                characters = await context.Characters.Cast<ICharacter>().ToListAsync();
            }
            return characters;
        }

        public async Task<ICharacter> GetByIdAsync(int id)
        {
            ICharacter character;
            using (MovieCharactersContext context = new())
            {
                character = await context.Characters.FindAsync(id);
            }
            return character;
        }

        public Task<int> UpdateAsync(ICharacter entity)
        {
            throw new NotImplementedException();
        }
    }
}
