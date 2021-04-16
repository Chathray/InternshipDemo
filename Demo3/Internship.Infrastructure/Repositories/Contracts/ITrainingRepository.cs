namespace Internship.Infrastructure
{
    public interface ITrainingRepository : IRepositoryBase<Training>
    {
        Training GetTrainingByIntern(int trainingId);
        string GetTrainingContent(int id);
    }
}
