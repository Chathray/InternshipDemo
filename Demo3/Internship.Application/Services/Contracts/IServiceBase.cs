using System.Collections.Generic;

namespace Internship.Application
{
    public interface IServiceBase<M, E>
    {
        M GetOne(int id);
        IList<M> GetAll();
        bool Update(M obj);
        bool Create(M obj);
        bool Delete(int id);
    }
}
