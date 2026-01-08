using UserArchiveService.Domain;
using UserArchiveService.Services.Dto;

namespace UserArchiveService.Services.Profile;

public class UserMovieArchiveProfile : AutoMapper.Profile
{
    public UserMovieArchiveProfile()
    {
        CreateMap<UserMovieArchive, UserMovieArchiveDto>().ReverseMap();
    }
}