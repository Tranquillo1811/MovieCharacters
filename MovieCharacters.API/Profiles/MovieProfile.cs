using AutoMapper;
using MovieCharacters.BLL.Models;
using System.Linq;

namespace MovieCharacters.API.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDto>()
                .ForMember(mdto => mdto.Franchise, opt => opt.MapFrom(m => m.FranchiseId))
                .ForMember(mdto => mdto.Characters, opt => opt.MapFrom(m => m.Characters.Select(c => c.Id).ToArray()))
                .ReverseMap();
        }
    }
}
