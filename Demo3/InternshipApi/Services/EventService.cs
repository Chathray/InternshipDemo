using Internship.Data;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace InternshipApi.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRespository;
        public EventService(IEventRepository eventRespository)
        {
            _eventRespository = eventRespository;
        }

        public Task<IReadOnlyList<Event>> GetAllAsync()
        {
            return _eventRespository.GetAllAsync();
        }

        public Task<int> GetCountAsync()
        {
            return _eventRespository.GetCountAsync();
        }

        public DataTable GetEventsIntern()
        {
            return _eventRespository.GetEventsIntern();
        }

        public string GetJson()
        {
            return _eventRespository.GetJson();
        }

        public bool InsertEvent(Event aEvent)
        {
            return _eventRespository.InsertEvent(aEvent);
        }
    }
}
