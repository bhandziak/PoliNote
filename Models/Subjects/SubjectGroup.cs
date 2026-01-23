namespace PoliNote.Models.Subjects
{
    public class SubjectGroup
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SubjectId { get; set; } // FK
        public Subject Subject { get; set; } = null!;
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
        public TimeSpan Duration { get; set; } // czas trwania zajęć

        // Enrollments
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }

    public enum Frequency
    {
        Weekly = 1,
        EveryTwoWeeks = 2,
        EveryFourWeeks = 4
    }
}
