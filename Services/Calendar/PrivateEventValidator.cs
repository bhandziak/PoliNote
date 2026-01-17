using FluentValidation;
using PoliNote.DTOs.PrivateCalendar;

namespace PoliNote.Services.Calendar
{
    public class PrivateEventValidator : AbstractValidator<PrivateEventRequestDto>
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
