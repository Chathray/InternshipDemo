using Internship.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternshipApi.Services
{
    public interface IInternService
    {
        Task<IReadOnlyList<Intern>> GetAllAsync();
        Task<int> GetCountAsync();

        public bool InsertIntern(Intern model);
        public bool RemoveIntern(int id);
        public bool UpdateIntern(Intern model);
        public string GetInternInfo(int id);
        public IList<Intern> GetInternByPage(int page, int size);
        public IList<InternViewModel> GetInternByPage(int page, int size, string sort);
    }
}