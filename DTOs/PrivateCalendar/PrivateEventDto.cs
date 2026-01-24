using PoliNote.DTOs.PrivateCalendar;
using System.Text.Json.Serialization;

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

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PrivateEventType? EventType { get; set; }
    }
}
