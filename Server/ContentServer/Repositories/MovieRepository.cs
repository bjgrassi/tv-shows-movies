using ContentService.Domain;
using Shared.Repository;

namespace ContentService.Repositories;

public class MovieRepository : UnitOfWork<Movie>, IMovieRepository
{
    public MovieRepository(ContentDbContext context) : base(context)
    {
        
    }
}