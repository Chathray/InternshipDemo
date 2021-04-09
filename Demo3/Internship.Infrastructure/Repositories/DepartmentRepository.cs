using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Internship.Infrastructure
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly DataContext _context;


        public DepartmentRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public bool InsertSharedTraining(int sharedId, int depId)
        {
            var boss = _context.Departments.Find(depId);

            if (boss is null) return false;

            var array = boss.SharedTrainings;

            if (array is not null && array.Length > 0)
            {
                if (!array.Contains("," + sharedId) && !array.Contains("" + sharedId))
                    array = array.Insert(array.Length, "," + sharedId);
            }
            else array = sharedId + "";

            return _context.Database.GetDbConnection()
                .ExecNonQuery($"CALL SetSharedTraining({depId},'{array}')");
        }
    }
}
