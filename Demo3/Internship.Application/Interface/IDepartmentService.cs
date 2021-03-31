using Internship.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Internship.Application
{
    public interface IDepartmentService
    {
        Task<IReadOnlyList<DepartmentModel>> GetAllAsync();
        Task<int> GetCountAsync();

    }
}