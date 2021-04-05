using Internship.Infrastructure;
using System.Collections.Generic;

namespace Internship.Application
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRespository;
        public OrganizationService(IOrganizationRepository organizationRespository)
        {
            _organizationRespository = organizationRespository;
        }

        public IList<OrganizationModel> GetAll()
        {
            var org = _organizationRespository.GetAll();
            return ObjectMapper.Mapper.Map<IList<Organization>, IList<OrganizationModel>>(org);
        }


        public bool UpdateOrganization(OrganizationModel model)
        {
            var obj = ObjectMapper.Mapper.Map<Organization>(model);
            return _organizationRespository.Update(obj);
        }

        public bool DeleteOrganization(int id)
        {
            return _organizationRespository.Delete(id);
        }
    }
}
