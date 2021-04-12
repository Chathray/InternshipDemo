using System.Collections.Generic;

namespace Internship.Application
{
    public interface IDepartmentService
    {
        IList<DepartmentModel> GetAll();

        bool UpdateDepartment(DepartmentModel model);
        bool Delete(int id);
        bool InsertSharedTraining(int sharedId, int depId);
    }
}