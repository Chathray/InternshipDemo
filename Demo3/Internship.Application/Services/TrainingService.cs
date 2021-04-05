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

        public bool DeleteTraining(int id)
        {
            return _trainingRespository.Delete(id);
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

        public string GetTrainingContent(int id)
        {
           return _trainingRespository.GetTrainingContent(id);
        }

        public bool InsertTraining(TrainingModel model)
        {
            var obj = ObjectMapper.Mapper.Map<Training>(model);
            return _trainingRespository.Insert(obj);
        }

        public bool UpdateTraining(TrainingModel model)
        {
            var obj = ObjectMapper.Mapper.Map<Training>(model);
            return _trainingRespository.Update(obj);
        }
    }
}
