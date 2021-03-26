namespace Internship.Data
{
    public interface ITrainingRepository : IRepository<Training>
    {
        Training GetTrainingByIntern(int trainingId);
    }
}
