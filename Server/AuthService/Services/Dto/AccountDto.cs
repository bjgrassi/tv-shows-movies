namespace AuthService.Services.Dto;

public class AccountDto
{
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public string? Password { get; set; }
    public int? RoleID { get; set; }
}