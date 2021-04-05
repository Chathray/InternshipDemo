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
    }
}
