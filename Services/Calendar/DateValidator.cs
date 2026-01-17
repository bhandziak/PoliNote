using System.ComponentModel.DataAnnotations;

namespace PoliNote.Services.Calendar
{
    public class DateValidator
    {
        public void ValidateOrThrow(DateTime? date)
        {
            if (!date.HasValue)
            {
                throw new ValidationException("Date is required.");
            }

            if (date.Value == default(DateTime))
            {
                throw new ValidationException("Invalid date value.");
            }

            if (date.Value.Year <= 2000)
            {
                throw new ValidationException("Date must be after year 2000.");
            }
        }
    }
}
