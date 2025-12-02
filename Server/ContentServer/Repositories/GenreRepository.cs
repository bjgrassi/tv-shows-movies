using ContentService.Domain;
using Shared.Repository;

namespace ContentService.Repositories;
public class GenreRepository : UnitOfWork<Genre>, IGenreRepository
{
    public GenreRepository(ContentDbContext context) : base(context)
    {
        
    }
}