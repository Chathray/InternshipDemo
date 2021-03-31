using System.Collections.Generic;
using System.Threading.Tasks;

namespace Internship.Infrastructure
{
    public interface IRepository<T> where T : EntityBase
    {
        public Task<IList<T>> GetAllAsync();
        public Task<int> GetCountAsync();
        public IList<T> GetAll();
        public int GetCount();
    }
}
