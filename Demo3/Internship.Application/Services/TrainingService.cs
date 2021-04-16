using Internship.Infrastructure;

namespace Internship.Application
{
    public class TrainingService : ServiceBase<TrainingModel, Training>, ITrainingService
    {
        private readonly ITrainingRepository _trainingRespository;
        public TrainingService(ITrainingRepository trainingRespository) : base(trainingRespository)
        {
            _trainingRespository = trainingRespository;
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

    }
}
