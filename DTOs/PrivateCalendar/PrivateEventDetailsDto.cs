namespace PoliNote.DTOs.PrivateCalendar
{
    public class PrivateEventDetailsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string? Location { get; set; }
        public PrivateEventType EventType { get; set; }
    }
}
