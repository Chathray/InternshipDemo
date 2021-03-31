using Internship.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Internship.Application
{
    public interface ITrainingService
    {
        Task<IReadOnlyList<TrainingModel>> GetAllAsync();
        Task<int> GetCountAsync();
        TrainingModel GetTrainingByIntern(int trainingId);
    }
}