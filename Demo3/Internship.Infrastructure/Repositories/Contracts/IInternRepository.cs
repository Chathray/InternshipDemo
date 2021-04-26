using System.Collections.Generic;
using System.Data;

namespace Internship.Infrastructure
{
    public interface IInternRepository : IRepositoryBase<Intern>
    {
        string GetInternInfo(int id);
        DataSet GetInternModelList(int page, int size, int sort, int search_on, string search_string);
        DataSet GetInternModelList(int currentPage, int pageSize, int sort, int search_on, string search_string, int on_passed, int date_filter, string start_date, string end_date);

        DataTable GetInternByPage(int page, int size, string sort);
        IList<Intern> GetInternByPage(int page, int size);
        string GetInternDetail(int id);
        IList<Training> GetJointTrainings(int internId);
        string GetWhitelist();
    }
}
