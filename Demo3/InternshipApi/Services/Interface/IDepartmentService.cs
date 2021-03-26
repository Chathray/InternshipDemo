using Internship.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternshipApi.Services
{
    public interface IDepartmentService
    {
        Task<IReadOnlyList<Department>> GetAllAsync();
        Task<int> GetCountAsync();

    }
}