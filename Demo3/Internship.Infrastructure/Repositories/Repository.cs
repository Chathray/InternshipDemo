using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
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
            return _context.Database.GetDbConnection()
                .QueryAsync<T>("SELECT * FROM " + typeof(T).Name.ToLowerInvariant() + "s")
                .Result.AsList();
        }

        public bool Update(T obj)
        {
            _context.Set<T>().Update(obj);
            return _context.SaveChanges() > 0;
        }

        public bool Insert(T obj)
        {
            _context.Set<T>().Add(obj);
            return _context.SaveChanges() > 0;
        }

        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public bool Delete(int id)
        {
            var obj = _context.Set<T>().Find(id);

            _context.Set<T>().Remove(obj);
            return _context.SaveChanges() > 0;
        }

        public int Count(Type type)
        {
            var count = _context.Database.GetDbConnection()
                .ExecuteScalar("SELECT Count(*) FROM " + type.Name.ToLower() + "s");

            return Convert.ToInt32(count);
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public int CountByIndex(int stt)
        {
            return stt switch
            {
                1 => Count(typeof(Department)),
                2 => Count(typeof(Event)),
                3 => Count(typeof(EventType)),
                4 => Count(typeof(Intern)),
                5 => Count(typeof(Organization)),
                6 => Count(typeof(Point)),
                7 => Count(typeof(Question)),
                8 => Count(typeof(Training)),
                9 => Count(typeof(User)),
                _ => 0
            };
        }
    }
}
