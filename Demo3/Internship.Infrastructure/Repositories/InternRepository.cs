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
                .ExecReaders($"CALL GetInternList({(page - 1) * size},{size},{sort},{search_on},'{search_string}')");
        }
        public DataSet GetInternModelList(int page, int size, int sort, int search_on, string search_string, int on_passed, int date_filter, string start_date, string end_date)
        {
            return _context.Database.GetDbConnection()
                .ExecReaders($"CALL GetInternListWithFilter({on_passed},{date_filter},'{start_date}','{end_date}',{(page - 1) * size},{size},{sort},{search_on},'{search_string}')");
        }



        public DataTable GetInternByPage(int page, int size, string sort)
        {
            return _context.Database.GetDbConnection()
                .ExecReader($"CALL GetInternList(" +
                $"{(page - 1) * size},{size},{sort})");
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
    }
}
