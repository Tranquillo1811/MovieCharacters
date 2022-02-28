using AutoMapper;
using MovieCharacters.BLL.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovieCharacters.API.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<ICharacter, CharacterDto>()
                .ReverseMap();
        }

    }
}
