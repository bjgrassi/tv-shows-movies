using ContentService.Domain;
using Shared.Repository;

namespace ContentService.Repositories;

public interface IMovieRepository : IRepository<Movie>
{
    Task AttachGenres(IEnumerable<Genre> genres);
    Task UpdateGenres(Movie movie);
}