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


    }
}
