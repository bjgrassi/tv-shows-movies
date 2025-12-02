using ContentService.Domain;
using Shared.Repository;

namespace ContentService.Repositories;
public class GenreMovieRepository : UnitOfWork<GenreMovie>, IGenreMovieRepository
{
    public GenreMovieRepository(ContentDbContext context) : base(context)
    {
        
    }
}