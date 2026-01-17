using PoliNote.Models;

namespace PoliNote.DTOs.Users
{
    public class UserDto
    {
        public string Username { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public UserRole Role { get; set; }
    }
}
