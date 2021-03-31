using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internship.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public IList<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public int GetCount()
        {
            return _context.Set<T>().Count();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }
    }
}
