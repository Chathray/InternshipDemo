using Internship.Infrastructure;

namespace Internship.Application
{
    public class OrganizationService : ServiceBase<OrganizationModel, Organization>, IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRespository;
        public OrganizationService(IOrganizationRepository organizationRespository) : base(organizationRespository)
        {
            _organizationRespository = organizationRespository;
        }


    }
}
