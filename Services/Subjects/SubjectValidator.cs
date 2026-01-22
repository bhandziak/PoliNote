using PoliNote.Models.Subjects;
using System.ComponentModel.DataAnnotations;

namespace PoliNote.Services.Subjects
{
    public class SubjectValidator : IDataValidator<Subject>
    {
        public void ValidateOrThrow(Subject subject)
        {
            if (string.IsNullOrWhiteSpace(subject.Name))
                throw new ValidationException("Subject name is required.");

            if (subject.Etcs < 0 || subject.Etcs > 30)
                throw new ValidationException("ECTS points must be between 0 and 30.");

            if (string.IsNullOrWhiteSpace(subject.LecturerName))
                throw new ValidationException("Lecturer name is required.");

            if (!Uri.IsWellFormedUriString(subject.SyllabusUrl, UriKind.Absolute))
                throw new ValidationException("Invalid Syllabus URL format.");
        }
    }
}
