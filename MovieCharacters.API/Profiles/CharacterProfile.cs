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
            CreateMap<Character, CharacterDto>()
                .ForMember(cdto => cdto.Movies, 
                    opt => opt.MapFrom(c => c.Movies.Select(c => c.Id))
                )
                .ReverseMap();
        }

    }
}
