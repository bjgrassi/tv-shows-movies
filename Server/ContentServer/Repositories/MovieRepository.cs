using ContentService.Domain;
using Microsoft.EntityFrameworkCore;
using Shared.Repository;

namespace ContentService.Repositories;

public class MovieRepository : UnitOfWork<Movie>, IMovieRepository
{
    private readonly IGenreMovieRepository _genreMovieRepository;
    public MovieRepository(ContentDbContext context, IGenreMovieRepository genreMovieRepository) : base(context)
    {
        _genreMovieRepository = genreMovieRepository;
    }

    public async Task AttachGenres(IEnumerable<Genre> genres)
    {
        var genresSet = this.Context.Set<Genre>();
        foreach (var genre in genres) {
            genresSet.Attach(genre);
        }
    }

    public async Task UpdateGenres(Movie movie)
    {
        var currentGenreIds = await Context.Set<GenreMovie>()
            .Where(gm => gm.MovieID == movie.MovieID)
            .Select(gm => gm.GenreID)
            .ToListAsync();

        var newGenreIds = movie.Genres?.Select(g => g.GenreID).ToList() ?? new List<int>();

        // Sets make diff operations extremely fast
        var currentSet = new HashSet<int>(currentGenreIds);
        var newSet = new HashSet<int>(newGenreIds);

        var toRemove = currentSet.Except(newSet);
        var toAdd = newSet.Except(currentSet);

        foreach (var genreId in toRemove)
        {
            var entity = new GenreMovie { MovieID = movie.MovieID, GenreID = genreId };
            await _genreMovieRepository.Delete(entity);
        }

        foreach (var genreId in toAdd)
        {
            var entity = new GenreMovie { MovieID = movie.MovieID, GenreID = genreId };
            await _genreMovieRepository.Create(entity);
        }
    }
}