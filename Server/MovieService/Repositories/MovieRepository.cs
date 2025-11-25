using MovieService.Domain;
using Shared.Repository;

namespace MovieService.Repositories;

public class MovieRepository : UnitOfWork<Movie>, IMovieRepository
{
    public MovieRepository(MovieDbContext context) : base(context)
    {
        
    }
}