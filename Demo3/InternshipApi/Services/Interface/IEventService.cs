using Internship.Data;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace InternshipApi.Services
{
    public interface IEventService
    {
        Task<IReadOnlyList<Event>> GetAllAsync();
        Task<int> GetCountAsync();

        DataTable GetEventsIntern();
        string GetJson();
        bool InsertEvent(Event aEvent);
    }
}