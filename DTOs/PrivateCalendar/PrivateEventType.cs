using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PoliNote.DTOs.PrivateCalendar
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PrivateEventType
    {
        Exam,
        Project,
        Meeting,
        Subject,
        Other
    }
}
