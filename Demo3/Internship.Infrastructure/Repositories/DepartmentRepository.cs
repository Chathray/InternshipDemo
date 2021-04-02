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

        public bool Delete(int id)
        {
            var obj = _context.Departments.Single(o => o.DepartmentId == id);
            _context.Remove(obj);
            return _context.SaveChanges() > 0;
        }
    }
}
