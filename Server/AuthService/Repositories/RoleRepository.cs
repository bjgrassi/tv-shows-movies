using AuthService.Domain;
using Shared.Repository;

namespace AuthService.Repositories;

public class RoleRepository : UnitOfWork<Role>, IRoleRepository
{
    public RoleRepository(AuthDbContext context) : base(context)
    {
        
    }
}