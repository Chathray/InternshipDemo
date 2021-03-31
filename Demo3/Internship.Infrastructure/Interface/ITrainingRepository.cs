namespace Internship.Infrastructure
{
    public interface ITrainingRepository : IRepository<Training>
    {
        Training GetTrainingByIntern(int trainingId);
    }
}
