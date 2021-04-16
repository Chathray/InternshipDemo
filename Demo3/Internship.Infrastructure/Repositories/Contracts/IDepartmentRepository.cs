namespace Internship.Infrastructure
{
    public interface IDepartmentRepository : IRepositoryBase<Department>
    {
        bool InsertSharedTraining(int sharedId, int depId);
    }
}
