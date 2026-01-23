using PoliNote.Models.Subjects;

namespace PoliNote.DTOs.Subjects.Responses
{
    public class SubjectGroupDetailsDto
    {
        public Guid Id { get; set; }
        public bool IsEnrolled { get; set; }

        public string Name { get; set; } = null!;
        public int Etcs { get; set; }
        public string SyllabusUrl { get; set; } = null!;

        public string LecturerName { get; set; } = null!;
        public string GroupName { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string TeacherName { get; set; } = null!;

        public string Frequency { get; set; } = null!;
        public string StartTime { get; set; } = null!;
        public string EndTime { get; set; } = null!;

    }
}
