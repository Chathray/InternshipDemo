using Internship.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Internship.Application
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRespository;
        public OrganizationService(IOrganizationRepository organizationRespository)
        {
            _organizationRespository = organizationRespository;
        }

        public Task<IReadOnlyList<OrganizationModel>> GetAllAsync()
        {
            var org = _organizationRespository.GetAllAsync();
            return ObjectMapper.Mapper.Map<Task<IReadOnlyList<OrganizationModel>>>(org);
        }

        public Task<int> GetCountAsync()
        {
            return _organizationRespository.GetCountAsync();
        }
    }
}
