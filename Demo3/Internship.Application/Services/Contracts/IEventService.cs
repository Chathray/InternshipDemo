using Internship.Infrastructure;
using System.Data;

namespace Internship.Application
{
    public interface IEventService : IServiceBase<EventModel, Event>
    {
        DataTable GetEventsIntern();
        string GetJson();
        bool InsertEvent(EventModel model);
    }
}