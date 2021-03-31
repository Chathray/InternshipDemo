using Internship.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Internship.Application
{
    public interface IInternshipPointService
    {
        Task<IReadOnlyList<InternshipPointModel>> GetAllAsync();
        Task<int> GetCountAsync();

    }
}