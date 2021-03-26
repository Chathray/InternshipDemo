using Internship.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternshipApi.Services
{
    public interface ITrainingService
    {
        Task<IReadOnlyList<Training>> GetAllAsync();
        Task<int> GetCountAsync();
        Training GetTrainingByIntern(int trainingId);
    }
}