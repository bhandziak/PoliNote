namespace PoliNote.DTOs.PublicCalendar
{
    public class PublicEventDetailsDto
    {
        public Guid Id { get; set; }
        public string CreatedByUsername { get; set; } = null!;
        public string? CreatedByNameAndSurname { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? Location { get; set; }
    }
}
