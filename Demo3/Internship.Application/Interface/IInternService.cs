using System.Collections.Generic;
using System.Data;

namespace Internship.Application
{
    public interface IInternService
    {
        IList<InternModel> GetAll();


        public bool InsertIntern(InternModel model);
        public bool DeleteIntern(int id);
        public bool UpdateIntern(InternModel model);
        public string GetInternInfo(int id);
        public string GetInternDetail(int id);
        public IList<InternModel> GetInternByPage(int page, int size);
        public IList<InternListModel> GetInternByPage(int page, int size, string sort);
        DataSet GetInternByPage(int currentPage, int pageSize, int sort, int search_on, string search_string);
        DataSet GetInternByPage(int currentPage, int pageSize, int sort, int search_on, string search_string, int on_passed, int date_filter, string start_date, string end_date);
    }
}