using PoliNote.DTOs.Subjects.Requests;
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

        }
    }
}
