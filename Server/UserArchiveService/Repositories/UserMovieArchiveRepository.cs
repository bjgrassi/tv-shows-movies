using UserArchiveService.Domain;
using Shared.Repository;

namespace UserArchiveService.Repositories;

public class UserMovieArchiveRepository : UnitOfWork<UserMovieArchive>, IUserMovieArchiveRepository
{
    public UserMovieArchiveRepository(UserArchiveDbContext context) : base(context)
    {
    }
}