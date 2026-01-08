using UserArchiveService.Services.Dto;

namespace UserArchiveService.Services;

public interface IUserMovieArchiveService
{
    Task<List<UserMovieArchiveDto>?> GetAllUserMovies(int userID);
}