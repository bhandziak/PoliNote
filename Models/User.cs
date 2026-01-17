namespace PoliNote.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string PasswordHash { get; set; } = null!;
    public UserRole Role { get; set; }
}
