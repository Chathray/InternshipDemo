using System.Linq;

namespace Internship.Infrastructure
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly DataContext _context;
        private readonly DataProvider _provider;

        public DepartmentRepository(DataContext context, DataProvider provider) : base(context)
        {
            _context = context;
            _provider = provider;
        }

        public bool Delete(int id)
        {
            var obj = _context.Departments.Single(o => o.DepartmentId == id);
            _context.Remove(obj);
            return _context.SaveChanges() > 0;

        }

        public bool Update(Department obj)
        {
            _context.Departments.Update(obj);
            return _context.SaveChanges() > 0;
        }
    }
}
