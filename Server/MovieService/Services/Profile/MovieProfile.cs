using MovieService.Domain;
using MovieService.Services.Dto;

namespace MovieService.Services.Profile;

public class MovieProfile : AutoMapper.Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, MovieDto>().ReverseMap();
    }
}