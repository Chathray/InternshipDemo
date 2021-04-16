namespace Internship.Infrastructure
{
    public class EventTypeRepository : RepositoryBase<EventType>, IEventTypeRepository
    {
        private readonly RepositoryContext _context;


        public EventTypeRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
    }
}
