using System.Collections.Generic;

namespace Internship.Application
{
    public interface IDepartmentService
    {
        IList<DepartmentModel> GetAll();

        bool UpdateDepartment(DepartmentModel model);
        bool DeleteDepartment(int id);
        bool InsertSharedTraining(int sharedId, int depId);
    }
}