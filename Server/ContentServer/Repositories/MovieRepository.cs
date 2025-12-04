using ContentService.Domain;
using Shared.Repository;

namespace ContentService.Repositories;

public class MovieRepository : UnitOfWork<Movie>, IMovieRepository
{
    public MovieRepository(ContentDbContext context) : base(context)
    {
        
    }

    public async Task AttachGenres(Movie movie)
    {
        var genresSet = this.Context.Set<Genre>();
        foreach (var genre in movie.Genres!) {
            genresSet.Attach(genre);
        }
    }
}