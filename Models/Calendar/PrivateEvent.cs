using PoliNote.Models.Users;

namespace PoliNote.Models.Calendar
{
    public class PrivateEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CreatedByUserId { get; set; } // FK
        public User? CreatedByUser { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string? Location { get; set; }
        public PrivateEventType EventType { get; set; }
    }
}
