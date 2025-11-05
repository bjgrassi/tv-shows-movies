using AuthService.Services.Dto;

namespace AuthService.Services;

public interface IAccountService
{
    Task Create(AccountDto account);
    Task Update(AccountDto account);
    Task Delete(AccountDto account);
    Task<List<AccountDto>> GetAll();
    Task<AccountDto> Get(string email);
    Task<AccountLoginDto> Login(string email, string password);
}