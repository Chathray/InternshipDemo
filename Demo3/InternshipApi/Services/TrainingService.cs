using Internship.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternshipApi.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRespository;
        public TrainingService(ITrainingRepository trainingRespository)
        {
            _trainingRespository = trainingRespository;
        }

        public Task<IReadOnlyList<Training>> GetAllAsync()
        {
            return _trainingRespository.GetAllAsync();
        }

        public Task<int> GetCountAsync()
        {
            return _trainingRespository.GetCountAsync();
        }

        public Training GetTrainingByIntern(int trainingId)
        {
            return _trainingRespository.GetTrainingByIntern(trainingId);
        }
    }
}
