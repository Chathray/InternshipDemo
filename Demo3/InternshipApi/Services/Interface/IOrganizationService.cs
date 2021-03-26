using Internship.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternshipApi.Services
{
    public interface IOrganizationService
    {
        Task<IReadOnlyList<Organization>> GetAllAsync();
        Task<int> GetCountAsync();
    }
}