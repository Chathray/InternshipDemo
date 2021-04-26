using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;

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
