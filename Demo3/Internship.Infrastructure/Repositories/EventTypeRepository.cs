namespace Internship.Infrastructure
{
    public class EventTypeRepository : RepositoryBase<EventType>, IEventTypeRepository
    {
        public EventTypeRepository(DataContext context) : base(context)
        { }
    }
}
