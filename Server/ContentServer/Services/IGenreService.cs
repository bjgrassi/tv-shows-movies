using ContentService.Services.Dto;

namespace ContentService.Services;

public interface IGenreService
{
    Task<List<GenreDto>?> GetAll();
    Task<GenreDto?> GetById(int movieID);
    Task Create(GenreDto movie);
    Task Update(GenreDto movie);
    Task Delete(GenreDto movie);
}