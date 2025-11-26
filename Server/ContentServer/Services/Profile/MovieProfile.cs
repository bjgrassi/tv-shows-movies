using ContentController.Domain;
using ContentService.Services.Dto;

namespace ContentService.Services.Profile;

public class MovieProfile : AutoMapper.Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, MovieDto>().ReverseMap();
    }
}