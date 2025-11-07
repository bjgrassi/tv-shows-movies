using AuthService.Domain;
using AuthService.Services.Dto;

namespace AuthService.Services.Profile;

public class AccountProfile : AutoMapper.Profile
{
    public AccountProfile()
    {
        CreateMap<Account, AccountDto>();
        CreateMap<AccountDto, Account>();
        CreateMap<Account, AccountLoginDto>();
    }
}