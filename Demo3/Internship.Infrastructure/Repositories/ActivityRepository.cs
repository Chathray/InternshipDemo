namespace Internship.Infrastructure
{
    public class ActivityRepository : RepositoryBase<Activity>, IActivityRepository
    {
        private readonly DataContext _context;


        public ActivityRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
