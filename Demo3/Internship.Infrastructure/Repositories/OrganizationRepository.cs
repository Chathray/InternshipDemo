using System.Linq;

namespace Internship.Infrastructure
{
    public class OrganizationRepository : Repository<Organization>, IOrganizationRepository
    {
        private readonly DataContext _context;

        public OrganizationRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {
            var obj = _context.Organizations.Single(o => o.OrganizationId == id);
            _context.Remove(obj);
            return _context.SaveChanges() > 0;
        }
    }
}
