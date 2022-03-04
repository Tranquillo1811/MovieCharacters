using Microsoft.EntityFrameworkCore;
using MovieCharacters.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MovieCharacters.DAL.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly MovieCharactersContext _context;

        public CharacterRepository(MovieCharactersContext context)
        {
            _context = context;
        }

        /// <summary>
        /// adds a character to the Db
        /// </summary>
        /// <param name="entity">Character to be added to the Db</param>
        /// <returns>newly added character</returns>
        public async Task<Character> AddAsync(Character entity)
        {
            Character characterResult = null;
            try
            {
                characterResult = (await _context.Characters.AddAsync(entity)).Entity;
                int intResult = await _context.SaveChangesAsync();
            }
            catch
            {
                //TODO: not quite sure, how to actually handle this...
            }
            return characterResult;
        }

        /// <summary>
        /// deletes Character with respective Id
        /// </summary>
        /// <param name="CharacterId">id of the character to be deleted</param>
        /// <returns>id of the deleted character</returns>
        public async Task<int> DeleteByIdAsync(int CharacterId)
        {
            Character deleteCharcter = await GetByIdAsync(CharacterId);
            try
            {
                _context.Characters.Remove(deleteCharcter);
                await _context.SaveChangesAsync();
            }
            catch
            {
                //TODO: not quite sure, how to actually handle this...
            }
            return CharacterId;
        }

        /// <summary>
        /// gets all characters from Db
        /// </summary>
        /// <returns>List of all characters from Character Db table</returns>
        public async Task<IEnumerable<Character>> GetAllAsync()
        {
            List<Character> characters = new();
            try
            {
                characters = await _context.Characters.Include(c => c.Movies).ToListAsync();
            }
            catch
            {
                //TODO: not quite sure, how to actually handle this...
            }
            return characters;
        }

        /// <summary>
        /// get character with particular Id from character table
        /// </summary>
        /// <param name="id">id of the character to be selected</param>
        /// <returns>Character with given id or null if no such character exists</returns>
        public async Task<Character> GetByIdAsync(int id)
        {
            Character character = null;
            try
            {
                character = await _context.Characters.Include(c => c.Movies).FirstOrDefaultAsync(c => c.Id == id);
            }
            catch
            {
                //TODO: not quite sure, how to actually handle this...
            }
            return character;
        }

        /// <summary>
        /// updates a character in Character Db table
        /// </summary>
        /// <param name="entity">character to be updated</param>
        /// <returns>updated character</returns>
        public async Task<Character> UpdateAsync(Character entity)
        {
            Character characterResult;

            _context.Entry(entity).State = EntityState.Modified;

            int intResult = 0;
            try
            {
                intResult = await _context.SaveChangesAsync();
            }
            catch
            {
                //TODO: not quite sure, how to actually handle this...
            }

            if (intResult == 0)
                return null;

            characterResult = entity;

            return characterResult;
        }
    }
}
