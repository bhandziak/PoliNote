using PoliNote.Repositories.Subjects;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PoliNote.Services.Notes
{
    public class NoteValidator
    {
        private readonly EnrollmentRepository _enrollmentRepo;
        private readonly SubjectGroupRepository _subjectGroupRepo;

        public NoteValidator(
            EnrollmentRepository enrollmentRepository,
            SubjectGroupRepository subjectGroupRepository)
        {
            _enrollmentRepo = enrollmentRepository;
            _subjectGroupRepo = subjectGroupRepository;
        }

        public async Task ValidateAccessOrThrowAsync(Guid userId, Guid subjectGroupId, DateTime? date)
        {
            var targetDate = DateOnly.FromDateTime(date!.Value);

            if (!await _subjectGroupRepo.ExistsAsync(subjectGroupId, targetDate))
            {
                throw new ValidationException($"Subject group {subjectGroupId} not found.");
            }

            if (!await _enrollmentRepo.IsUserEnrolledAsync(userId, subjectGroupId))
            {
                throw new ValidationException("You are not enrolled to this subject.");
            }
        }
    }
}
