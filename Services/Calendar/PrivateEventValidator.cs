using PoliNote.DTOs.PrivateCalendar;
using System.ComponentModel.DataAnnotations;

namespace PoliNote.Services.Calendar
{
    public class PrivateEventValidator : IDataValidator<PrivateEventRequestDto>
    {
        public void ValidateOrThrow(PrivateEventRequestDto request)
        {
            if (request.Date.Date < DateTime.Today)
            {
                throw new ValidationException("You cannot add or update events with a past date.");
            }
        }
    }
}
