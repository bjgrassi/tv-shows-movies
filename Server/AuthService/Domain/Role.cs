namespace AuthService.Domain;

public class Role 
{
    public required int RoleID { get; set; }
    public required string TypeName { get; set; }
    public required string Description { get; set; }
}