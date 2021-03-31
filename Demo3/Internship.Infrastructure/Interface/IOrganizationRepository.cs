namespace Internship.Infrastructure
{
    public interface IOrganizationRepository : IRepository<Organization>
    {
        bool Update(Organization obj);
        bool Delete(int id);
    }
}
