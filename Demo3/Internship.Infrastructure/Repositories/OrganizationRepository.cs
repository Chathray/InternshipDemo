namespace Internship.Infrastructure
{
    public class OrganizationRepository : RepositoryBase<Organization>, IOrganizationRepository
    {
        private readonly RepositoryContext _context;

        public OrganizationRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
    }
}
