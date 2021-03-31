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

    }
}
