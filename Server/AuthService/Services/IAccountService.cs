using AuthService.Services.Dto;

namespace AuthService.Services;

public interface IAccountService
{
    Task<List<AccountDto>?> GetAll();
    Task<AccountDto?> GetById(int accountID);
    Task Create(AccountDto account);
    Task Update(AccountDto account);
    Task Delete(AccountDto account);
    Task<AccountLoginDto> Login(string email, string password);
}