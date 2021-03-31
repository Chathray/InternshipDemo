using System.Collections.Generic;
using System.Data;

namespace Internship.Application
{
    public interface IEventService
    {
        IList<EventModel> GetAll();
        int GetCount();

        DataTable GetEventsIntern();
        string GetJson();
        bool InsertEvent(EventModel model);
    }
}