using PoliNote.Models.Calendar;
using PoliNote.Models.Notes;
using PoliNote.Models.Subjects;

namespace PoliNote.Models.Users;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = null!;
    public string Email { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public bool IsActivated { get; set; }
    public string ActivationToken { get; set; } = string.Empty;

    // relations
    public ICollection<PublicEvent> PublicEvents { get; set; } = new List<PublicEvent>();
    public ICollection<PrivateEvent> PrivateEvents { get; set; } = new List<PrivateEvent>();
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public ICollection<Note> Notes { get; set; } = new List<Note>();
}
