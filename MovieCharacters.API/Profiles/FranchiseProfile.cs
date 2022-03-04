using AutoMapper;
using MovieCharacters.BLL.Models;
using System.Linq;

namespace MovieCharacters.API.Profiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<Franchise, FranchiseReadDto>()
                .ForMember(fdto => fdto.Movies, opt => opt.MapFrom(f => f.Movies.Select(m => m.Id).ToArray()))
                .ReverseMap();
            CreateMap<Franchise, FranchiseReadDto>().ReverseMap();
        }
    }
}
