using System.Linq;

namespace Internship.Infrastructure
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly DataContext _context;
        

        public DepartmentRepository(DataContext context) : base(context)
        {
            _context = context;

        }
    }
}
