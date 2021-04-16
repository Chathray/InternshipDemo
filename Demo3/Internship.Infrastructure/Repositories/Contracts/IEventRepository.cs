using System.Data;

namespace Internship.Infrastructure
{
    public interface IEventRepository : IRepositoryBase<Event>
    {
        DataTable GetJointEvents();
        string GetJson();
        bool CheckOne(string title);
        bool UpdateByTitle(Event aEvent);
    }
}
