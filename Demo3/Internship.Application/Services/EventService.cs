using Internship.Infrastructure;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Internship.Application
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRespository;
        public EventService(IEventRepository eventRespository)
        {
            _eventRespository = eventRespository;
        }

        public Task<IReadOnlyList<EventModel>> GetAllAsync()
        {
            var even= _eventRespository.GetAllAsync();
            return ObjectMapper.Mapper.Map<Task<IReadOnlyList<EventModel>>>(even);
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

        public bool InsertEvent(EventModel model)
        {
            Event aEvent = ObjectMapper.Mapper.Map<Event>(model);

            var dateArray = model.Deadline.Split(" - ");
            // Ngoại lệ ngày đơn
            try
            {
                aEvent.Start = dateArray[0];
                aEvent.End = dateArray[1];
            }
            catch { }

            switch (model.Type)
            {
                case "fullcalendar-custom-event-hs-team":
                    aEvent.Type = "Personal";
                    break;
                case "fullcalendar-custom-event-holidays":
                    aEvent.Type = "Holidays";
                    break;
                case "fullcalendar-custom-event-tasks":
                    aEvent.Type = "Tasks";
                    break;
                case "fullcalendar-custom-event-reminders":
                    aEvent.Type = "Reminders";
                    break;
            }
            aEvent.ClassName = model.Type;
            
            return _eventRespository.InsertEvent(aEvent);
        }
    }
}
