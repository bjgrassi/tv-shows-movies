using ContentService.Services.Dto;

namespace ContentService.Services;

public interface ISerieService
{
    Task<List<SerieDto>?> GetAll();
    Task<SerieDto?> GetById(int SerieID);
    Task Create(SerieDto Serie);
    Task Update(SerieDto Serie);
    Task Delete(SerieDto Serie);
}