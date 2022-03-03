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
        /// <summary>
        /// adds a character to the Db
        /// </summary>
        /// <param name="entity">Character to be added to the Db</param>
        /// <returns>newly added character</returns>
        public async Task<Character> AddAsync(Character entity)
        {
            Character characterResult = null;
            using (MovieCharactersContext context = new())
            {
                try
                {
                    characterResult = (await context.Characters.AddAsync(entity)).Entity;
                    int intResult = await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    //TODO: not quite sure, how to actually handle this...
                }
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
            using(MovieCharactersContext context = new())
            {
                try
                {
                    context.Characters.Remove(deleteCharcter);
                    await context.SaveChangesAsync();
                }
                catch(Exception ex)
                {
                    //TODO: not quite sure, how to actually handle this...
                }
            }
            return CharacterId;
        }

        public async Task<IEnumerable<Character>> FindAllAsync(Expression<Func<Character, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// gets all characters from Db
        /// </summary>
        /// <returns>List of all characters from Character Db table</returns>
        public async Task<IEnumerable<Character>> GetAllAsync()
        {
            List<Character> characters = new ();
            using(MovieCharactersContext context = new ())
            {
                try
                {
                    characters = await context.Characters.Cast<Character>().ToListAsync();
                }
                catch(Exception ex)
                {
                    //TODO: not quite sure, how to actually handle this...
                }
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
            using (MovieCharactersContext context = new())
            {
                try
                {
                    character = await context.Characters.FindAsync(id);
                }
                catch (Exception ex)
                {
                    //TODO: not quite sure, how to actually handle this...
                }
            }
            return character;
        }

        /// <summary>
        /// updates a character in Character Db table
        /// </summary>
        /// <param name="entity">character to be updated</param>
        /// <returns>updated character</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Character> UpdateAsync(Character entity)
        {
            Character characterResult;
            using (MovieCharactersContext context = new())
            {
                //Character editCharacter = null;
                //try
                //{
                //    editCharacter = context.Characters.Find(entity.Id);
                //}
                //catch(Exception ex)
                //{
                //    //TODO: not quite sure, how to actually handle this...
                //}
                //if (editCharacter == null)
                //    return null;

                ////--- change all properties
                //editCharacter.FullName = entity.FullName;
                //editCharacter.Alias = entity.Alias;
                //editCharacter.PictureUrl = entity.PictureUrl;
                //editCharacter.Gender = entity.Gender;
                //editCharacter.Movies = ((Character)entity).Movies;

                context.Entry(entity).State = EntityState.Modified;

                int intResult = 0;
                try
                {
                    intResult = await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    //TODO: not quite sure, how to actually handle this...
                }

                if (intResult == 0)
                    return null;
                //characterResult = editCharacter;
                characterResult = entity;
            }
            return characterResult;
        }
    }
}
