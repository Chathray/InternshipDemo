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

        public IList<EventTypeModel> GetAll()
        {
            var obj = _evenTypeRespository.GetAll();
            return ObjectMapper.Mapper.Map<IList<EventType>, IList<EventTypeModel>>(obj);
        }
    }
}
