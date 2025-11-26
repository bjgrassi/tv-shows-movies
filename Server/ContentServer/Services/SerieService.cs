using ContentController.Domain;
using ContentController.Repositories;
using ContentService.Services.Dto;
using AutoMapper;

namespace ContentService.Services;

public class SerieService : ISerieService
{
    private readonly ISerieRepository _serieRepository;
    private readonly IMapper _mapper;

    public SerieService(ISerieRepository serieRepository, IMapper mapper)
    {
        _serieRepository = serieRepository;
        _mapper = mapper;
    }
    public async Task<List<SerieDto>?> GetAll()
    {
        var result = await _serieRepository.GetAll();
        if (result.Any())
            return _mapper.Map<List<SerieDto>>(result);
        return [];
    }
    public async Task<SerieDto?> GetById(int serieID)
    {
        var result = await _serieRepository.GetById(serieID);

        if (result == null)
            throw new ArgumentException("Serie not found.", "notfound");

        return _mapper.Map<SerieDto>(result);
    }
    public async Task Create(SerieDto movieDto)
    {
        if (movieDto.SerieID > 0)
            throw new ArgumentException("Serie already has id.", "duplicate");
        var movieEntity = _mapper.Map<Serie>(movieDto);
        await _serieRepository.Create(movieEntity);
    }

    public async Task Update(SerieDto movieDto)
    {
        if (movieDto.SerieID <= 0)
            throw new ArgumentException("Serie id is invalid.", "invalid");
        
        var movieItem = await GetById(movieDto.SerieID);
        if (movieItem != null)
        {
            var movie = _mapper.Map<Serie>(movieDto);
            await _serieRepository.Update(movie);
        }
    }
    public async Task Delete(SerieDto movieDto)
    {
        if (movieDto.SerieID <= 0)
            throw new ArgumentException("Serie is invalid.", "invalid");

        var movieItem = await GetById(movieDto.SerieID);
        if (movieItem != null)
        {
            var movieEntity = _mapper.Map<Serie>(movieDto);
            await _serieRepository.Delete(movieEntity);
        }
    }
}