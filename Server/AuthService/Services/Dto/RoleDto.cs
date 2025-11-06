using System.ComponentModel.DataAnnotations;

namespace AuthService.Services.Dto;

public class RoleDto
{
    public int RoleID { get; set; }
    public string? TypeName { get; set; }
    public string? Description { get; set; }

    public IEnumerable<AccountDto> Accounts { get; set; } = new List<AccountDto>();
}