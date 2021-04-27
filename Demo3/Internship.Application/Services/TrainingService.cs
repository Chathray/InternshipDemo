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
    }
}
