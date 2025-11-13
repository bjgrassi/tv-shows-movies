using AuthService.Services.Dto;

namespace AuthService.Services;

public interface IAccountService
{
    Task<List<AccountDtoGetRole>?> GetAll();
    Task<AccountDtoGetRole?> GetById(int accountID);
    Task Create(AccountDtoCreate account);
    Task Update(AccountDtoUpdate account);
    Task Delete(AccountDtoUpdate account);
    Task<AccountLoginDto> Login(string email, string password);
}