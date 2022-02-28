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
            List<ICharacter> characters = new ();
            using(MovieCharactersContext context = new ())
            {
                characters = context.Characters.Cast<ICharacter>().ToList();
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
