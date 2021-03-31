using Internship.Infrastructure;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Internship.Application
{
    public interface IInternService
    {
        Task<IReadOnlyList<InternModel>> GetAllAsync();
        Task<int> GetCountAsync();

        public bool InsertIntern(InternModel model);
        public bool RemoveIntern(int id);
        public bool UpdateIntern(InternModel model);
        public string GetInternInfo(int id);
        public string GetInternDetail(int id);
        public IList<InternModel> GetInternByPage(int page, int size);
        public IList<InternListModel> GetInternByPage(int page, int size, string sort);
        DataSet GetInternModelList(int currentPage, int pageSize, int sort, int search_on, string search_string);
    }
}