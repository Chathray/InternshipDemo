using Internship.Infrastructure;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Internship.Application
{
    public interface IEventService
    {
        Task<IReadOnlyList<EventModel>> GetAllAsync();
        Task<int> GetCountAsync();

        DataTable GetEventsIntern();
        string GetJson();
        bool InsertEvent(EventModel model);
    }
}