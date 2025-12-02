using Microsoft.EntityFrameworkCore;

using ContentService.Domain;
using ContentService.Repositories;
using ContentService.Services.Dto;
using AutoMapper;

namespace ContentService.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;

    public MovieService(IMovieRepository movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }
    public async Task<List<MovieDto>?> GetAll()
    {
        var result = await _movieRepository.GetAll();
        if (result.Any())
            return _mapper.Map<List<MovieDto>>(result);
        return [];
    }
    public async Task<MovieDto?> GetById(int movieID)
    {
        var query = _movieRepository.GetQueryable();
        query = query.Include(movie => movie.Genres);

        var result = await query.Where(movie => movie.MovieID == movieID).FirstOrDefaultAsync();

        if (result == null)
            throw new ArgumentException("Movie not found.", "notfound");

        return _mapper.Map<MovieDto>(result);
    }
    public async Task Create(MovieDto movieDto)
    {
        if (movieDto.MovieID > 0)
            throw new ArgumentException("Movie already has id.", "duplicate");
        var movieEntity = _mapper.Map<Movie>(movieDto);
        await _movieRepository.Create(movieEntity);
    }

    public async Task Update(MovieDto movieDto)
    {
        if (movieDto.MovieID <= 0)
            throw new ArgumentException("Movie id is invalid.", "invalid");
        
        var movieItem = await GetById(movieDto.MovieID);
        if (movieItem != null)
        {
            var movie = _mapper.Map<Movie>(movieDto);
            await _movieRepository.Update(movie);
        }
    }
    public async Task Delete(MovieDto movieDto)
    {
        if (movieDto.MovieID <= 0)
            throw new ArgumentException("Movie is invalid.", "invalid");

        var movieItem = await GetById(movieDto.MovieID);
        if (movieItem != null)
        {
            var movieEntity = _mapper.Map<Movie>(movieDto);
            await _movieRepository.Delete(movieEntity);
        }
    }
}