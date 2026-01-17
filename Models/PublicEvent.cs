using System.ComponentModel.DataAnnotations;

namespace PoliNote.Models
{
    public class PublicEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int CreatedByUserId { get; set; } // FK
        public User? CreatedByUser { get; set; }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? Location { get; set; }
    }
}
