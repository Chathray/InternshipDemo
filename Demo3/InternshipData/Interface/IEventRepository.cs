using System.Data;

namespace Internship.Data
{
    public interface IEventRepository : IRepository<Event>
    {
        DataTable GetEventsIntern();
        string GetJson();
        bool InsertEvent(Event @event);
    }
}
