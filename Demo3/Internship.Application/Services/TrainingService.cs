using Internship.Infrastructure;
using System.Collections.Generic;

namespace Internship.Application
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRespository;
        public TrainingService(ITrainingRepository trainingRespository)
        {
            _trainingRespository = trainingRespository;
        }

        public IList<TrainingModel> GetAll()
        {
            var tra = _trainingRespository.GetAll();
            return ObjectMapper.Mapper.Map<IList<Training>, IList<TrainingModel>>(tra);
        }

        public TrainingModel GetTrainingByIntern(int trainingId)
        {
            var obj = _trainingRespository.GetTrainingByIntern(trainingId);
            return ObjectMapper.Mapper.Map<TrainingModel>(obj);
        }

        public bool InsertTraining(TrainingModel model)
        {
            var obj = ObjectMapper.Mapper.Map<Training>(model);
            return _trainingRespository.InsertTraining(obj);
        }
    }
}
