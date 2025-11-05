using AuthService.Domain;
using AuthService.Services.Dto;

namespace AuthService.Services.Profile;

public class RoleProfile : AutoMapper.Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleDto>().ReverseMap();
    }
}