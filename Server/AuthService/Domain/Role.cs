namespace AuthService.Domain;

public class Role 
{
    public int RoleID { get; set; }
    public string? TypeName { get; set; }
    public string? Description { get; set; }

    public IEnumerable<Account> Accounts { get; set; } = new List<Account>();
}