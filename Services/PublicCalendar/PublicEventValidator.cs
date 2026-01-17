using PoliNote.DTOs.PublicCalendar;
using System.ComponentModel.DataAnnotations;

namespace PoliNote.Services.PublicCalendar
{
    public class PublicEventValidator
    {
        public void ValidateEvent(PublicEventRequestDto request)
        {
            if (request.EndTime <= request.StartTime)
            {
                throw new ValidationException("The end time must be later than the start time.");
            }

            if (request.Date.Date < DateTime.Today)
            {
                throw new ValidationException("You cannot add or update events with a past date.");
            }
        }
    }
}
