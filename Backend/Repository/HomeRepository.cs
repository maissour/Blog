
using Repository.Data;

namespace Repository
{
    public class HomeRepository
    {
        private readonly ApplicationDbContext context;
        public HomeRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public string Test()
        {
            return "ok";
        }
    }
}
