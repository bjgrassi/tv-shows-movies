using AuthService.Services.Dto;

namespace AuthService.Services;

public interface IRoleService
{
    Task<List<RoleDto>> GetAll();
}