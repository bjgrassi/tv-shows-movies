using ContentController.Domain;
using Shared.Repository;

namespace ContentController.Repositories;

public class MovieRepository : UnitOfWork<Movie>, IMovieRepository
{
    public MovieRepository(MovieDbContext context) : base(context)
    {
        
    }
}