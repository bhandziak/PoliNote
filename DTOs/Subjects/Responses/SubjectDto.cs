namespace PoliNote.DTOs.Subjects.Responses
{
    public class SubjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Etcs { get; set; }
        public string SyllabusUrl { get; set; } = null!;
        public string LecturerName { get; set; } = null!;
    }
}
