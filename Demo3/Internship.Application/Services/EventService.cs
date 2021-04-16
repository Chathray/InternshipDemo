using Internship.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;

namespace Internship.Application
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRespository;
        public EventService(IEventRepository eventRespository)
        {
            _eventRespository = eventRespository;
        }

        public IList<EventModel> GetAll()
        {
            var evens = _eventRespository.GetAll();
            return ObjectMapper.Mapper.Map<IList<Event>, IList<EventModel>>(evens);
        }

        public DataTable GetEventsIntern()
        {
            return _eventRespository.GetJointEvents();
        }

        public string GetJson()
        {
            return _eventRespository.GetJson();
        }

        public bool InsertEvent(EventModel model)
        {
            Event obj = ObjectMapper.Mapper.Map<Event>(model);

            var dateArray = model.Deadline.Split(" - ");
            // Ngoại lệ ngày đơn
            try
            {
                obj.Start = DateTime.ParseExact(dateArray[0], "dd/MM/yyyy", null).ToString("yyyy-MM-dd");
                obj.End = DateTime.ParseExact(dateArray[1], "dd/MM/yyyy", null).ToString("yyyy-MM-dd");
            }
            catch { obj.End = obj.Start; }

            switch (model.Type)
            {
                case "fullcalendar-custom-event-hs-team":
                    obj.Type = "Personal";
                    break;
                case "fullcalendar-custom-event-holidays":
                    obj.Type = "Holidays";
                    break;
                case "fullcalendar-custom-event-tasks":
                    obj.Type = "Tasks";
                    break;
                case "fullcalendar-custom-event-reminders":
                    obj.Type = "Reminders";
                    break;
            }
            obj.ClassName = model.Type;

            if (_eventRespository.CheckOne(obj.Title))

                return _eventRespository.Create(obj);
            else
                return _eventRespository.UpdateByTitle(obj);

        }
    }
}
