namespace AuthService.Domain;

public class Account
{
    public int AccountID { get; set; }
    public required string Email { get; set; }
    public required string FullName { get; set; }
    public required string Password { get; set; }
    public int? RoleID { get; set; }

    public Role? Role { get; set; }
}