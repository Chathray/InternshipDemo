namespace Internship.Infrastructure
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        bool Delete(int id);
    }
}
