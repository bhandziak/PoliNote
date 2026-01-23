namespace PoliNote.DTOs.Subjects.Responses
{
    public class SubjectGroupDto
    {
        public Guid Id { get; set; }
        public bool IsEnrolled { get; set; }
        public string Name { get; set; } = null!;
        public int Etcs { get; set; }

        public string TeacherName { get; set; } = null!;
        public string GroupName { get; set; } = null!;

        public string StartTime { get; set; } = null!; // godzina rozpoczęcia
        public string EndTime { get; set; } = null!;
    }
}
