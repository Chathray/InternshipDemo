namespace Internship.Infrastructure
{
    public class InternshipPointRepository : Repository<InternshipPoint>, IInternshipPointRepository
    {
        private readonly DataContext _context;
        private readonly DataProvider _provider;

        public InternshipPointRepository(DataContext context, DataProvider provider) : base(context)
        {
            _context = context;
            _provider = provider;
        }

    }
}
