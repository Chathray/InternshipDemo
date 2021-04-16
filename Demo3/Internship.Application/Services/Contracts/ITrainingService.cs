using Internship.Infrastructure;

namespace Internship.Application
{
    public interface ITrainingService : IServiceBase<TrainingModel, Training>
    {

        TrainingModel GetTrainingByIntern(int trainingId);
        string GetTrainingContent(int id);
    }
}