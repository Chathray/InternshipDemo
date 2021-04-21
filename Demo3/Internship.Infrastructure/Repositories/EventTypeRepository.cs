namespace Internship.Infrastructure
{
    public class EventTypeRepository : RepositoryBase<EventType>, IEventTypeRepository
    {
        private readonly DataContext _context;


        public EventTypeRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
