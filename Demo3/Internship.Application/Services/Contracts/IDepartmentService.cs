using Internship.Infrastructure;

namespace Internship.Application
{
    public interface IDepartmentService : IServiceBase<DepartmentModel, Department>
    {
        bool InsertSharedTraining(int sharedId, int depId);
    }
}