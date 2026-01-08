using ContentService.Domain;
using ContentService.Services.Dto;

namespace ContentService.Services.Profile;

public class MovieProfile : AutoMapper.Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, MovieDto>()
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres));
        CreateMap<MovieDto, Movie>();
        CreateMap<Genre, GenreDto>().ReverseMap();
    }
}