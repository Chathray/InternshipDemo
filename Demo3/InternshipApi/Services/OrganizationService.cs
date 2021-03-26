using Internship.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternshipApi.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRespository;
        public OrganizationService(IOrganizationRepository organizationRespository)
        {
            _organizationRespository = organizationRespository;
        }

        public Task<IReadOnlyList<Organization>> GetAllAsync()
        {
            return _organizationRespository.GetAllAsync();
        }

        public Task<int> GetCountAsync()
        {
            return _organizationRespository.GetCountAsync();
        }
    }
}
