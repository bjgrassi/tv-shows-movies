using Microsoft.EntityFrameworkCore;

using ContentService.Domain;
using ContentService.Repositories;
using ContentService.Services.Dto;
using AutoMapper;

namespace ContentService.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;

    public GenreService(IGenreRepository genreRepository, IMapper mapper)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
    }
    public async Task<List<GenreDto>?> GetAll()
    {
        var result = await _genreRepository.GetAll();
        return _mapper.Map<List<GenreDto>>(result);
    }
    public async Task<GenreDto?> GetById(int genreID)
    {
        var query = _genreRepository.GetQueryable();
        var result = await query.Where(genre => genre.GenreID == genreID).FirstOrDefaultAsync();
        
        if (result == null)
            throw new ArgumentException("Genre not found.", "notfound");
        return _mapper.Map<GenreDto>(result);
    }
    public async Task Create(GenreDto genreDto)
    {
        if (genreDto.GenreID > 0)
            throw new ArgumentException("Genre already has id.", "duplicate");
        
        var genre = _mapper.Map<Genre>(genreDto);
        await _genreRepository.Create(genre);
    }

    public async Task Update(GenreDto genreDto)
    {
        if (genreDto.GenreID <= 0)
            throw new ArgumentException("Genre id is invalid.", "invalid");
        
        var genreItem = await GetById(genreDto.GenreID);
        if (genreItem != null)
        {
            var genreEntity = _mapper.Map<Genre>(genreDto);
            await _genreRepository.Update(genreEntity);
        }
    }
    public async Task Delete(GenreDto genreDto)
    {
        if (genreDto.GenreID <= 0)
            throw new ArgumentException("Genre is invalid.", "invalid");

        var genreItem = await GetById(genreDto.GenreID);
        if (genreItem != null)
        {
            var genreEntity = _mapper.Map<Genre>(genreDto);
            await _genreRepository.Delete(genreEntity);
        }
    }
}