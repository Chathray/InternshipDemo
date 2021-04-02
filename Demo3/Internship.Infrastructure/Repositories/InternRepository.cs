using Dapper;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Internship.Infrastructure
{
    public class InternRespository : Repository<Intern>, IInternRepository
    {
        private readonly DataContext _context;

        public InternRespository(DataContext context) : base(context)
        {
            _context = context;
        }

        public string GetInternInfo(int id)
        {
            return _context.Database.GetDbConnection()
                .ExecuteScalar($"CALL GetInternInfo('{id}')").ToString();
        }


        public string GetInternDetail(int id)
        {
            return _context.Database.GetDbConnection()
                .ExecuteScalar($"CALL GetInternDetail('{id}')").ToString();
        }

        public DataSet GetInternModelList(int page, int size, int sort, int search_on, string search_string)
        {
            return _context.Database.GetDbConnection()
                .ExecReaders($"CALL GetInternList({(page - 1) * size},{size},'{sort}',{search_on},'{search_string}')");
        }

        public DataTable GetInternByPage(int page, int size, string sort)
        {
            return _context.Database.GetDbConnection()
                .ExecReader($"CALL GetInternList(" +
                $"{(page - 1) * size},{size},'{sort}')");
        }

        public IList<Intern> GetInternByPage(int page, int size)
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
            return _context.Database.GetDbConnection()
                .Execute($"CALL InsertIntern(" +
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
                        $"'{model.DepartmentId}')") > 0;
        }

        public bool UpdateIntern(Intern model)
        {
            return _context.Database.GetDbConnection()
                .Execute($"CALL UpdateIntern(" +
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
                        $"'{model.DepartmentId}')") > 0;
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
