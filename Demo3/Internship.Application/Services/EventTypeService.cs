using Internship.Infrastructure;
using System.Collections.Generic;

namespace Internship.Application
{
    public class EventTypeService : IEventTypeService
    {
        private readonly IEventTypeRepository _evenTypeRespository;
        public EventTypeService(IEventTypeRepository evenTypeRespository)
        {
            _evenTypeRespository = evenTypeRespository;
        }

        public int GetCount()
        {
            return _evenTypeRespository.GetCount();
        }

        public IList<EventTypeModel> GetAll()
        {
            var dep = _evenTypeRespository.GetAll();
            var model = ObjectMapper.Mapper.Map<IList<EventType>, IList<EventTypeModel>>(dep);
            return model;
        }
    }
}
