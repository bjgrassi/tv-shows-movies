using AuthService.Domain;
using AuthService.Services.Dto;

namespace AuthService.Services.Profile;

public class AccountProfile : AutoMapper.Profile
{
    public AccountProfile()
    {
        var regularId = 3; // Default role ID for regular users

        CreateMap<Account, AccountDto>();
        CreateMap<AccountDto, Account>()
            .ForMember(dest => dest.RoleID, opt => opt.MapFrom(src => src.RoleID != null ? src.RoleID : regularId))
            .ForMember(dest => dest.Role, opt => opt.Ignore());
    }
}