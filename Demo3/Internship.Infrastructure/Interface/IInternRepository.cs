using System.Collections.Generic;
using System.Data;

namespace Internship.Infrastructure
{
    public interface IInternRepository : IRepository<Intern>
    {
        public string GetInternInfo(int id);
        public DataSet GetInternModelList(int page, int size, int sort, int search_on, string search_string);
        public DataSet GetInternModelList(int currentPage, int pageSize, int sort, int search_on, string search_string, int on_passed, int date_filter, string start_date, string end_date);

        public DataTable GetInternByPage(int page, int size, string sort);
        public IList<Intern> GetInternByPage(int page, int size);
        string GetInternDetail(int id);
        public IList<Training> GetJointTrainings(int internId);
    }
}
