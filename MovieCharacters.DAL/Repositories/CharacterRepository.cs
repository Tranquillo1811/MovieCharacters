using Microsoft.EntityFrameworkCore;
using MovieCharacters.BLL.Models;
using MovieCharacters.DAL.Models;
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
        public async Task<ICharacter> AddAsync(ICharacter entity)
        {
            Character characterResult;
            using (MovieCharactersContext context = new())
            {
                characterResult = (await context.Characters.AddAsync((Character)entity)).Entity;
                int intResult = await context.SaveChangesAsync();
            }
            return characterResult;
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

        public Task<ICharacter> UpdateAsync(ICharacter entity)
        {
            throw new NotImplementedException();
        }
    }
}
