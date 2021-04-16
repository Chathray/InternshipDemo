using System.Collections.Generic;

namespace Internship.Application
{
    public interface IOrganizationService
    {
        IList<OrganizationModel> GetAll();

        bool UpdateOrganization(OrganizationModel model);
        bool Delete(int id);
    }
}