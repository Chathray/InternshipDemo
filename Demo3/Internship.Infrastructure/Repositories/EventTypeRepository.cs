namespace Internship.Infrastructure
{
    public class EventTypeRepository : Repository<EventType>, IEventTypeRepository
    {
        private readonly DataContext _context;
        private readonly DataProvider _provider;

        public EventTypeRepository(DataContext context, DataProvider provider) : base(context)
        {
            _context = context;
            _provider = provider;
        }

    }
}
