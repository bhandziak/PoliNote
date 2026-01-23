using PoliNote.Models.Calendar;
using PoliNote.Models.Subjects;

namespace PoliNote.Models.Users;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string PasswordHash { get; set; } = null!;
    public UserRole Role { get; set; }

    // relations
    public ICollection<PublicEvent> PublicEvents { get; set; } = new List<PublicEvent>();
    public ICollection<PrivateEvent> PrivateEvents { get; set; } = new List<PrivateEvent>();
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
