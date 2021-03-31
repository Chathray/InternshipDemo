using System.Collections.Generic;
using System.Data;

namespace Internship.Infrastructure
{
    public interface IInternRepository : IRepository<Intern>
    {
        public bool InsertIntern(Intern model);
        public bool RemoveIntern(int id);
        public bool UpdateIntern(Intern model);
        public string GetInternInfo(int id);
        public DataSet GetInternModelList(int page, int size, int sort, int search_on, string search_string);

        public IList<Intern> GetInternByPage(int page, int size);
        public DataTable GetInternByPage(int page, int size, string sort);
        string GetInternDetail(int id);
    }
}
