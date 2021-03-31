using System.Linq;

namespace Internship.Infrastructure
{
    public class OrganizationRepository : Repository<Organization>, IOrganizationRepository
    {
        private readonly DataContext _context;
        private readonly DataProvider _provider;

        public OrganizationRepository(DataContext context, DataProvider provider) : base(context)
        {
            _context = context;
            _provider = provider;
        }

        public bool Delete(int id)
        {
            var obj = _context.Organizations.Single(o => o.OrganizationId == id);
            _context.Remove(obj);
            return _context.SaveChanges() > 0;
        }

        public bool Update(Organization obj)
        {
            _context.Organizations.Update(obj);
            return _context.SaveChanges() > 0;
        }
    }
}
