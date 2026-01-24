namespace PoliNote.DTOs.Users
{
    public class ActivateRequestDto
    {
        public string Token { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
