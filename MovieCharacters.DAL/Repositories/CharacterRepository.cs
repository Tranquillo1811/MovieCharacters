using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MovieCharacters.BLL.Models;

namespace MovieCharacters.DAL.Models
{
    public class CharacterRepository : ICharacterRepository
    {
        public bool Add(ICharacter entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(ICharacter entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ICharacter> FindAll(Expression<Func<ICharacter, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ICharacter> GetAll()
        {
            List<Character> characters = new ();
            using(MovieCharactersContext context = new ())
            {
                characters = context.Characters.ToList();
            }
            return characters;
        }

        public ICharacter GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(ICharacter entity)
        {
            throw new NotImplementedException();
        }
    }
}
