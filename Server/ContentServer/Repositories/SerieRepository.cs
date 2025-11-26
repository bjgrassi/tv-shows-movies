using ContentController.Domain;
using Shared.Repository;

namespace ContentController.Repositories;

public class SerieRepository : UnitOfWork<Serie>, ISerieRepository
{
    public SerieRepository(ContentDbContext context) : base(context)
    {
        
    }
}