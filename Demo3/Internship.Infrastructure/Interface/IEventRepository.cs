using System.Data;

namespace Internship.Infrastructure
{
    public interface IEventRepository : IRepository<Event>
    {
        DataTable GetEventsIntern();
        string GetJson();
        bool InsertEvent(Event @event);
    }
}
