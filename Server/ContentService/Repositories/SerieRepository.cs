using ContentService.Domain;
using Shared.Repository;

namespace ContentService.Repositories;

public class SerieRepository : UnitOfWork<Serie>, ISerieRepository
{
    public SerieRepository(ContentDbContext context) : base(context)
    {
        
    }
}