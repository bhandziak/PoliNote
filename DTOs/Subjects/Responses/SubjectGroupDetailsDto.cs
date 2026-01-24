using PoliNote.DTOs.Subjects;

namespace PoliNote.DTOs.Subjects.Responses
{
    public class SubjectGroupDetailsDto
    {
        public Guid Id { get; set; }
        public bool IsEnrolled { get; set; }

        // subject
        public string Name { get; set; } = null!;
        public int Etcs { get; set; }
        public string SyllabusUrl { get; set; } = null!;
        public string LecturerName { get; set; } = null!;

        // group
        public string GroupName { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string TeacherName { get; set; } = null!;
        // time
        public string StartTime { get; set; } = null!;
        public string EndTime { get; set; } = null!;
        // date
        public string Frequency { get; set; } = null!;
        public DateOnly FirstOccurrence { get; set; }
        public DateOnly LastOccurrence { get; set; }
        public string DayOfWeek { get; set; } = null!;
        // presence
        public int? Absences { get; set; }
    }
}
