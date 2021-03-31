namespace Internship.Infrastructure
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        bool Update(Department obj);
        bool Delete(int id);
    }
}
