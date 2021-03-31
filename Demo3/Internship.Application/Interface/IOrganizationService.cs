using Internship.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Internship.Application
{
    public interface IOrganizationService
    {
        Task<IReadOnlyList<OrganizationModel>> GetAllAsync();
        Task<int> GetCountAsync();
    }
}