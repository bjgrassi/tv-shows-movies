using AuthService.Domain;
using Shared.Repository;

namespace AuthService.Repositories;

public class AccountRepository : UnitOfWork<Account>, IAccountRepository
{
    public AccountRepository(AuthDbContext context) : base(context)
    {
        
    }
}