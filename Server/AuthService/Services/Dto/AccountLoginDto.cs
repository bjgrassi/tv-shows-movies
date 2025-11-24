namespace AuthService.Services.Dto;
public class AccountLoginDto
{
    public int AccountID { get; set; }
    public required string Email { get; set; }
    public required string FullName { get; set; }
    public int? RoleID { get; set; }
}