using PoliNote.Models.Subjects;
using PoliNote.Models.Users;

namespace PoliNote.Models.Notes
{
    public class Note
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        // user
        public Guid UserId { get; set; } // FK
        public User User { get; set; } = null!;
        // subject
        public Guid SubjectGroupId { get; set; } // FK
        public SubjectGroup SubjectGroup { get; set; } = null!;
        public DateOnly TargetDate { get; set; } // target date
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
