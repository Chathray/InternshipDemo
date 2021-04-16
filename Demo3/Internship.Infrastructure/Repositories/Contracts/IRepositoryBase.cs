using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Internship.Infrastructure
{
    public interface IRepositoryBase<T>
    {
        Task<IList<T>> GetAllAsync();
        Task<int> GetCountAsync();
        IList<T> GetAll();
        bool Update(T obj);
        bool Create(T obj);
        T Get(int id);
        bool Delete(int id);
        int Count(Type type);
        int CountByIndex(int stt);
    }
}
