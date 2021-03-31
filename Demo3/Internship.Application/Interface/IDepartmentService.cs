using System.Collections.Generic;

namespace Internship.Application
{
    public interface IDepartmentService
    {
        IList<DepartmentModel> GetAll();
        int GetCount();
        bool UpdateDepartment(DepartmentModel model);
        bool DeleteDepartment(int id);
    }
}