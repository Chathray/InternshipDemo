using Internship.Infrastructure;

namespace Internship.Application
{
    public class DepartmentService : ServiceBase<DepartmentModel, Department>, IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public bool InsertSharedTraining(int sharedId, int depId)
        {
            return _departmentRepository.InsertSharedTraining(sharedId, depId);
        }
    }
}
