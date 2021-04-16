using System.Collections.Generic;

namespace Internship.Application
{
    public interface ITrainingService
    {
        IList<TrainingModel> GetAll();

        TrainingModel GetTrainingByIntern(int trainingId);
        bool InsertTraining(TrainingModel model);
        bool UpdateTraining(TrainingModel model);
        string GetTrainingContent(int id);
        bool Delete(int id);
    }
}