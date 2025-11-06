using AuthService.Domain;
using AuthService.Services.Dto;

namespace AuthService.Services.Profile;

public class AccountProfile : AutoMapper.Profile
{
    public AccountProfile()
    {
        CreateMap<Account, AccountDto>()
            .ForMember(a => a.RoleID, a => a.MapFrom(b => b.RoleID));
        CreateMap<AccountDto, Account>()
            .ForMember(dest => dest.RoleID, opt => opt.MapFrom(src => src.RoleID));
        CreateMap<Account, AccountLoginDto>();
    }
}