namespace Internship.Infrastructure
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        bool InsertSharedTraining(int sharedId, int depId);
    }
}
