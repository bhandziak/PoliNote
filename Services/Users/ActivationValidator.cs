using PoliNote.DTOs.Users;
using System.ComponentModel.DataAnnotations;

namespace PoliNote.Services.Users
{
    public class ActivationValidator : IDataValidator<ActivateRequestDto>
    {
        public void ValidateOrThrow(ActivateRequestDto request)
        {
            if (request.Password == null)
                throw new ValidationException("Password can't be empty");
            if (request.Password.Length < 6)
                throw new ValidationException("Password have to be at least 6 letter long");
        }
    }
}
