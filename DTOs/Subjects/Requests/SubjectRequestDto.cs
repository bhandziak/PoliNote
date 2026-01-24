namespace PoliNote.DTOs.Subjects.Requests
{
    public class SubjectRequestDto
    {
        public string Name { get; set; } = null!;
        public int Etcs { get; set; }
        public string SyllabusUrl { get; set; } = null!;
        public string LecturerName { get; set; } = null!;
    }
}
