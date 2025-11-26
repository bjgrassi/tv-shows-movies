using ContentController.Domain;
using ContentService.Services.Dto;

namespace ContentService.Services.Profile;

public class SerieProfile : AutoMapper.Profile
{
    public SerieProfile()
    {
        CreateMap<Serie, SerieDto>().ReverseMap();
    }
}