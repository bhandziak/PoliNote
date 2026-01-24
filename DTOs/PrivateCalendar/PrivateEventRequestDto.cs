using PoliNote.DTOs;

namespace PoliNote.DTOs.PrivateCalendar
{
    public class PrivateEventRequestDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string? Location { get; set; }
        public string? EventType { get; set; }
    }
}
