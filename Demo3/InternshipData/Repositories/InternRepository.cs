using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Internship.Data
{
    public class InternRespository : Repository<Intern>, IInternRespository
    {
        private readonly DataContext _context;
        private readonly DataProvider _provider;

        public InternRespository(DataContext context, DataProvider provider) : base(context)
        {
            _context = context;
            _provider = provider;
        }

        public string GetInternInfo(int id)
        {
            return _provider
                .ExecuteScalar($"CALL GetInternInfo('{id}')").ToString();
        }

        public DataTable GetInternByPage(int page, int size, string sort)
        {
            return _provider.ExecuteReader($"CALL GetInternList(" +
                $"{(page - 1) * size},{size},'{sort}')");
        }

        public IEnumerable<Intern> GetInternByPage(int page, int size)
        {
            return _context.Interns
                     .Include(b => b.Organizations)
                     .Include(b => b.Departments)
                     .Include(b => b.Users)
                     .Include(b => b.Trainings)
                     .OrderBy(i => i.InternId)
                     .ThenBy(i => i.FirstName)
                     .Skip((page - 1) * size)
                     .Take(size)
                     .ToList();
        }

        public bool InsertIntern(Intern model)
        {
            return _provider
                .ExecuteNonQuery($"CALL InsertIntern(" +
                        $"'{model.Email}', " +
                        $"'{model.Phone}', " +
                        $"'{model.FirstName}', " +
                        $"'{model.LastName}', " +
                        $"'{model.DateOfBirth}', " +
                        $"'{model.Gender}', " +
                        $"'{model.Duration}', " +
                        $"'{model.Type}', " +
                        $"'{model.Mentor}', " +
                        $"'{model.TrainingId}', " +
                        $"'{model.OrganizationId}', " +
                        $"'{model.DepartmentId}')");
        }

        public bool UpdateIntern(Intern model)
        {
            return _provider
                .ExecuteNonQuery($"CALL UpdateIntern(" +
                        $"'{model.InternId}', " +
                        $"'{model.Email}', " +
                        $"'{model.Phone}', " +
                        $"'{model.FirstName}', " +
                        $"'{model.LastName}', " +
                        $"'{model.DateOfBirth}', " +
                        $"'{model.Gender}', " +
                        $"'{model.Duration}', " +
                        $"'{model.Type}', " +
                        $"'{model.Mentor}', " +
                        $"'{model.TrainingId}', " +
                        $"'{model.OrganizationId}', " +
                        $"'{model.DepartmentId}')");
        }

        public bool RemoveIntern(int id)
        {
            var target = _context.Interns.FirstOrDefault(x => x.InternId == id);
            _context.Interns.Remove(target);
            var result = _context.SaveChanges();

            return result > 0;
        }
    }
}
