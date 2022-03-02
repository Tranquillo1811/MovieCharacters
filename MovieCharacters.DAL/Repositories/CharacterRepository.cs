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
        /// <summary>
        /// adds a character to the Db
        /// </summary>
        /// <param name="entity">Character to be added to the Db</param>
        /// <returns>newly added character</returns>
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

        /// <summary>
        /// gets all characters from Db
        /// </summary>
        /// <returns>List of all characters from Character Db table</returns>
        public async Task<IEnumerable<ICharacter>> GetAllAsync()
        {
            List<ICharacter> characters = new ();
            using(MovieCharactersContext context = new ())
            {
                characters = await context.Characters.Cast<ICharacter>().ToListAsync();
            }
            return characters;
        }

        /// <summary>
        /// get character with particular Id from character table
        /// </summary>
        /// <param name="id">id of the character to be selected</param>
        /// <returns>Character with given id or null if no such character exists</returns>
        public async Task<ICharacter> GetByIdAsync(int id)
        {
            ICharacter character;
            using (MovieCharactersContext context = new())
            {
                character = await context.Characters.FindAsync(id);
            }
            return character;
        }

        /// <summary>
        /// updates a character in Character Db table
        /// </summary>
        /// <param name="entity">character to be updated</param>
        /// <returns>updated character</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ICharacter> UpdateAsync(ICharacter entity)
        {
            Character characterResult;
            using (MovieCharactersContext context = new())
            {
                Character editCharacter = context.Characters.Find(entity.Id);
                if (editCharacter == null)
                    return null;
                //--- change all properties
                editCharacter.FullName = entity.FullName;
                editCharacter.Alias = entity.Alias;
                editCharacter.PictureUrl = entity.PictureUrl;
                editCharacter.Gender = entity.Gender;
                editCharacter.Movies = ((Character)entity).Movies;
                int intResult = await context.SaveChangesAsync();
                if (intResult == 0)
                    return null;
                characterResult = editCharacter;
            }
            return characterResult;
        }
    }
}
