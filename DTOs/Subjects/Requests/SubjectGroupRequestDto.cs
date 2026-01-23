using PoliNote.Models.Subjects;

namespace PoliNote.DTOs.Subjects.Requests
{
    public class SubjectGroupRequestDto
    {
        // group info
        public string GroupName { get; set; } = null!; // np. Laboratorium 12 / Wykład
        public string Location { get; set; } = null!; // np. sala 102
        public string TeacherName { get; set; } = null!;
        // date
        public Frequency Frequency { get; set; } //  np. raz na 1/2 tyg.
        public DateOnly FirstOccurrence { get; set; } // pierwsze zajęcie
        public DateOnly LastOccurrence { get; set; } // ostatnie zajęcie
        public DayOfWeek DayOfWeek { get; set; }
        // lesson
        public TimeOnly StartTime { get; set; } // godzina rozpoczęcia
        public int DurationMinutes { get; set; }
    }
}
