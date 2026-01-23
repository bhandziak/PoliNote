using PoliNote.DTOs.Subjects.Requests;
using PoliNote.Models.Subjects;
using System.ComponentModel.DataAnnotations;

namespace PoliNote.Services.Subjects
{
    public class SubjectGroupValidator : IDataValidator<SubjectGroupRequestDto>
    {
        public void ValidateOrThrow(SubjectGroupRequestDto request)
        {
            if (request.FirstOccurrence >= request.LastOccurrence)
                throw new ValidationException("First occurrence must be before last occurrence.");

            if (request.DurationMinutes <= 0)
                throw new ValidationException("Duration must be greater than 0 minutes.");

            // frequency
            if (!Enum.IsDefined(typeof(Frequency), request.Frequency))
                throw new ValidationException("Invalid frequency value.");
            // day of week
            if (!Enum.IsDefined(typeof(DayOfWeek), request.DayOfWeek))
                throw new ValidationException("Invalid day of week.");
        }
    }
}
