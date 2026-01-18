using PoliNote.Models;

namespace PoliNote.DTOs.PrivateCalendar
{
    public class PrivateEventDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime Date { get; set; }
        public string TimeString { get; set; } = null!;
        public string? Location { get; set; }
        public bool IsSubject { get; set; } = false;
        public PrivateEventType? EventType { get; set; }
    }
}
