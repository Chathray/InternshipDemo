namespace Internship.Infrastructure
{
    public class EventTypeRepository : Repository<EventType>, IEventTypeRepository
    {
        private readonly DataContext _context;
        

        public EventTypeRepository(DataContext context) : base(context)
        {
            _context = context;

        }

    }
}
