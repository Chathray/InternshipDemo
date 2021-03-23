using System.Collections.Generic;
using System.Threading.Tasks;

namespace Internship.Data
{
    public interface IRepository<T> where T : EntityBase
    {
        public Task<IReadOnlyList<T>> GetAllAsync();
        public Task<int> GetCountAsync();
    }
}
