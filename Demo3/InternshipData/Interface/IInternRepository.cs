using System.Collections.Generic;
using System.Data;

namespace Internship.Data
{
    public interface IInternRepository : IRepository<Intern>
    {
        public bool InsertIntern(Intern model);
        public bool RemoveIntern(int id);
        public bool UpdateIntern(Intern model);
        public string GetInternInfo(int id);
        public IList<Intern> GetInternByPage(int page, int size);
        public DataTable GetInternByPage(int page, int size, string sort);
    }
}
