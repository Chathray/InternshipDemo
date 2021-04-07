using Internship.Infrastructure;
using System.Collections.Generic;

namespace Internship.Application
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRespository;
        public DepartmentService(IDepartmentRepository departmentRespository)
        {
            _departmentRespository = departmentRespository;
        }


        public IList<DepartmentModel> GetAll()
        {
            var dep = _departmentRespository.GetAll();
            var model = ObjectMapper.Mapper.Map<IList<Department>, IList<DepartmentModel>>(dep);
            return model;
        }

        public bool UpdateDepartment(DepartmentModel model)
        {
            var obj = ObjectMapper.Mapper.Map<Department>(model);
            return _departmentRespository.Update(obj);
        }

        public bool DeleteDepartment(int id)
        {
            return _departmentRespository.Delete(id);
        }

        public bool InsertSharedTraining(int sharedId, int depId)
        {
            return _departmentRespository.InsertSharedTraining(sharedId, depId);
        }
    }
}
