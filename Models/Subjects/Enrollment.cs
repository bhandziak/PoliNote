namespace PoliNote.Models.Subjects
{
    public class Enrollment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SubjectGroupId { get; set; } // FK
        public SubjectGroup SubjectGroup { get; set; } = null!;
        public Guid UserId { get; set; } // FK
        public User User { get; set; } = null!;
        public int? Absences { get; set; } = null;
    }
}
