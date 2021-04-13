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
            string search_on_p = search_on switch
            {
                1 => $"t1.InternId LIKE '{search_string}'",
                2 => $"t1.Email LIKE '{search_string}'",
                3 => $"t1.FirstName LIKE '{search_string}'",
                4 => $"t1.Phone LIKE '{search_string}'",
                5 => $"DepName LIKE '{search_string}'",
                6 => $"OrgName LIKE '{search_string}'",
                7 => $"TraName LIKE '{search_string}'",
                8 => $"t5.FirstName LIKE '{search_string}'",
                _ => "1",
            };

            string sort_p = sort switch
            {
                2 => "t1.Email",
                3 => "t1.FirstName",
                4 => "t1.Phone",
                5 => "DepName",
                6 => "OrgName",
                7 => "TraName",
                8 => "t5.FirstName",
                _ => "t1.InternId",
            };

            var p_type = $"{search_on_p} ORDER BY {sort_p} LIMIT {(page - 1) * size},{size}";

            return _context.Database.GetDbConnection()
                .ExecReaders($"CALL GetInternList({"\""}{p_type}{"\""})");
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

        public IList<Training> GetJointTrainings(int internId)
        {
            List<Training> result = new();

            string list_str = _context.Database.GetDbConnection()
                 .ExecuteScalar($"CALL GetJointTrainings({internId})").ToString();

            string[] splited = list_str.Split(',');

            var list_id = splited.Distinct().AsList();

            foreach (var training_id in list_id)
            {
                var step = _context.Trainings.Find(int.Parse(training_id));
                result.Add(step);
            }
            return result;
        }
    }
}
