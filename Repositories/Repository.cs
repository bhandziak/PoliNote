using PoliNote.Data;

namespace PoliNote.Repositories
{
    public class Repository
    {
        protected readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }
    }
}
