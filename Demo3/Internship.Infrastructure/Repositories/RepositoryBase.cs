using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Internship.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        private readonly RepositoryContext _context;

        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return !trackChanges ?
                _context.Set<T>()
                .Where(expression)
                .AsNoTracking()
                :
                _context.Set<T>()
                .Where(expression);
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            return !trackChanges ?
                _context.Set<T>()
                .AsNoTracking()
                :
                _context.Set<T>();
        }

        public T GetOne(int key)
        {
            return _context.Set<T>().Find(key);
        }

        public IList<T> GetAll()
        {
            return _context.Database.GetDbConnection()
                .QueryAsync<T>("SELECT * FROM " + typeof(T).Name.ToLowerInvariant() + "s")
                .Result.AsList();
        }

        public bool Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChanges() > 0;
        }

        public bool Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int key)
        {
            var obj = _context.Set<T>().Find(key);

            _context.Set<T>().Remove(obj);
            return _context.SaveChanges() > 0;
        }

        public int Count(Type type)
        {
            var count = _context.Database.GetDbConnection()
                .ExecuteScalar("SELECT Count(*) FROM " + type.Name.ToLower() + "s");

            return Convert.ToInt32(count);
        }

        public int CountByIndex(int index)
        {
            return index switch
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
