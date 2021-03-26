using Internship.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternshipApi.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRespository;
        public DepartmentService(IDepartmentRepository departmentRespository)
        {
            _departmentRespository = departmentRespository;
        }

        public Task<IReadOnlyList<Department>> GetAllAsync()
        {
            return _departmentRespository.GetAllAsync();
        }

        public Task<int> GetCountAsync()
        {
            return _departmentRespository.GetCountAsync();
        }
    }
}
