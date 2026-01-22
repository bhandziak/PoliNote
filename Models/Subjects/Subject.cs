namespace PoliNote.Models.Subjects
{
    public class Subject
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public int Etcs {  get; set; }
        public string SyllabusUrl { get; set; } = null!;
        public string LecturerName { get; set; } = null!;

        // subjectGroups
        public ICollection<SubjectGroup> Groups { get; set; } = new List<SubjectGroup>();
    }
}
