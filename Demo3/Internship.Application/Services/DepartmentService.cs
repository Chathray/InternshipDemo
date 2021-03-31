using Internship.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Internship.Application
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRespository;
        public DepartmentService(IDepartmentRepository departmentRespository)
        {
            _departmentRespository = departmentRespository;
        }

        public Task<IReadOnlyList<DepartmentModel>> GetAllAsync()
        {
            var dep = _departmentRespository.GetAllAsync();
            var model = ObjectMapper.Mapper.Map<Task<IReadOnlyList<DepartmentModel>>>(dep);
            return model;
        }

        public Task<int> GetCountAsync()
        {
            return _departmentRespository.GetCountAsync();
        }
    }
}
