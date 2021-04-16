using Internship.Infrastructure;

namespace Internship.Application
{
    public class EventTypeService : ServiceBase<EventTypeModel, EventType>, IEventTypeService
    {
        private readonly IEventTypeRepository _evenTypeRespository;
        public EventTypeService(IEventTypeRepository evenTypeRespository) : base(evenTypeRespository)
        {
            _evenTypeRespository = evenTypeRespository;
        }
    }
}
