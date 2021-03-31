using Internship.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Internship.Application
{
    public class EventTypeService : IEventTypeService
    {
        private readonly IEventTypeRepository _evenTypeRespository;
        public EventTypeService(IEventTypeRepository evenTypeRespository)
        {
            _evenTypeRespository = evenTypeRespository;
        }

        public Task<int> GetCountAsync()
        {
            return _evenTypeRespository.GetCountAsync();
        }

        public Task<IReadOnlyList<EventTypeModel>> GetAllAsync()
        {
            var dep = _evenTypeRespository.GetAllAsync();
            var model = ObjectMapper.Mapper.Map<Task<IReadOnlyList<EventTypeModel>>>(dep);
            return model;
        }
    }
}
