using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Internship.Infrastructure
{
    public interface IRepository<T> where T : EntityBase
    {
        public Task<IList<T>> GetAllAsync();
        public Task<int> GetCountAsync();
        public IList<T> GetAll();
        public bool Update(T obj);
        public bool Insert(T obj);
        public bool Delete(int id);
        public int Count(Type type);
        public int CountByIndex(int stt);
    }
}
