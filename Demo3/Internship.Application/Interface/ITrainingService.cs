using System.Collections.Generic;

namespace Internship.Application
{
    public interface ITrainingService
    {
        IList<TrainingModel> GetAll();
        int GetCount();
        TrainingModel GetTrainingByIntern(int trainingId);
        bool InsertTraining(TrainingModel model);
    }
}