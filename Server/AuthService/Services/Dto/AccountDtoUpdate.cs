using System.ComponentModel.DataAnnotations;

namespace AuthService.Services.Dto;

public class AccountDtoUpdate
{
    public int AccountID { get; set; }
    public required string Email { get; set; }
    public required string FullName { get; set; }
    public required string Password { get; set; }
    public int? RoleID { get; set; }
}