using ContentService.Services.Dto;

namespace ContentService.Services;

public interface IMovieService
{
    Task<List<MovieDto>?> GetAll();
    Task<MovieDto?> GetById(int movieID);
    Task Create(MovieDto movie);
    Task Update(MovieDto movie);
    Task Delete(MovieDto movie);
    Task<List<MovieDto>> SearchMoviesByIds(List<int> movieIds);
}