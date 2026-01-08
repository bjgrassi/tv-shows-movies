using Microsoft.EntityFrameworkCore;
using AutoMapper;

using UserArchiveService.Domain;
using UserArchiveService.Repositories;
using UserArchiveService.Services.Dto;

namespace UserArchiveService.Services;

public class UserMovieArchiveService : IUserMovieArchiveService
{
    private readonly IUserMovieArchiveRepository _userMovieArchiveRepository;
    private readonly IMapper _mapper;
    private readonly MovieServiceClient _movieServiceClient;

    public UserMovieArchiveService(IUserMovieArchiveRepository userMovieArchiveRepository, MovieServiceClient movieServiceClient, IMapper mapper)
    {
        _userMovieArchiveRepository = userMovieArchiveRepository;
        _movieServiceClient = movieServiceClient;
        _mapper = mapper;
    }
    public async Task<List<UserMovieArchiveDto>?> GetAllUserMovies(int userID)
    {
        var archiveData = await _userMovieArchiveRepository.GetQueryable().Where(x => x.UserAccountFK == userID).ToListAsync();
        var movieIds = archiveData.Select(x => x.MovieFK).Distinct().ToList();
        var movieDetails = await _movieServiceClient.SearchMoviesByIds(movieIds);

        var result = archiveData.Select(archive => new UserMovieArchiveDto
        {
            UserMovieArchiveID = archive.UserMovieArchiveID,
            MovieFK = archive.MovieFK,
            IsWatched = archive.IsWatched,
            Movie = movieDetails.FirstOrDefault(m => m.MovieID == archive.MovieFK)
        });
        
        if (result.Any())
            return _mapper.Map<List<UserMovieArchiveDto>>(result);
        return [];
    }
}