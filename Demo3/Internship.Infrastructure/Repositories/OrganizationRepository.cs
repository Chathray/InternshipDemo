namespace Internship.Infrastructure
{
    public class OrganizationRepository : RepositoryBase<Organization>, IOrganizationRepository
    {
        private readonly DataContext _context;

        public OrganizationRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
