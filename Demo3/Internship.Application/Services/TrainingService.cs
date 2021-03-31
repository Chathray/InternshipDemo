using Internship.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Internship.Application
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRespository;
        public TrainingService(ITrainingRepository trainingRespository)
        {
            _trainingRespository = trainingRespository;
        }

        public Task<IReadOnlyList<TrainingModel>> GetAllAsync()
        {
            var tra = _trainingRespository.GetAllAsync();
            return ObjectMapper.Mapper.Map<Task<IReadOnlyList<TrainingModel>>>(tra);
        }

        public Task<int> GetCountAsync()
        {
            return _trainingRespository.GetCountAsync();
        }

        public TrainingModel GetTrainingByIntern(int trainingId)
        {
            var obj = _trainingRespository.GetTrainingByIntern(trainingId);
            return ObjectMapper.Mapper.Map<TrainingModel>(obj);
        }
    }
}
