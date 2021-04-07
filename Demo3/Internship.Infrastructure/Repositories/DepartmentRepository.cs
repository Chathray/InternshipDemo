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
            return _context.Database.GetDbConnection()
                .ExecNonQuery($"CALL SetSharedTraining({sharedId},{depId})");
        }
    }
}
