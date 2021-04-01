using System.Collections.Generic;

namespace Internship.Application
{
    public interface IEventTypeService
    {
        IList<EventTypeModel> GetAll();

    }
}