namespace AuthService.Services.Dto;

public class AccountDtoCreate
{
    public required string Email { get; set; }
    public required string FullName { get; set; }
    public required string Password { get; set; }
    public int? RoleID { get; set; }
}