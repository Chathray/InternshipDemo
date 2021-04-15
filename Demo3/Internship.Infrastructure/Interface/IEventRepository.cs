using System.Collections.Generic;
using System.Data;

namespace Internship.Infrastructure
{
    public interface IEventRepository : IRepository<Event>
    {
        DataTable GetJointEvents();
        string GetJson();
        bool CheckOne(string title);
        bool UpdateByTitle(Event aEvent);
    }
}
