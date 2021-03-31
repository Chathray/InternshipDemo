using Internship.Infrastructure;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Internship.Application
{
    public interface IEventTypeService
    {
        Task<IReadOnlyList<EventTypeModel>> GetAllAsync();
        Task<int> GetCountAsync();
    }
}