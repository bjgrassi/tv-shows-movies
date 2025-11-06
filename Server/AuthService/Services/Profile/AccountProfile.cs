using AuthService.Domain;
using AuthService.Services.Dto;

namespace AuthService.Services.Profile;

public class AccountProfile : AutoMapper.Profile
{
    public AccountProfile()
    {
        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<Account, AccountDto>()
            .ForMember(dest => dest.RoleID, opt => opt.MapFrom(src => src.Role != null ? src.Role.RoleID : (int?)null))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role != null ? src.Role.TypeName : null))
            .ReverseMap()
            .ForMember(dest => dest.Role, opt => opt.Ignore());
        CreateMap<Account, AccountLoginDto>();
    }
}