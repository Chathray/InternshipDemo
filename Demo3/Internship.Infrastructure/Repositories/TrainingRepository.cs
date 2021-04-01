using System.Linq;

namespace Internship.Infrastructure
{
    public class TrainingRepository : Repository<Training>, ITrainingRepository
    {
        private readonly DataContext _context;
        private readonly DataProvider _provider;

        public TrainingRepository(DataContext context, DataProvider provider) : base(context)
        {
            _context = context;
            _provider = provider;
        }

        public Training GetTrainingByIntern(int id)
        {
            var trainingId = _context.Interns
                .Single(b => b.InternId == id).TrainingId;

            return _context.Trainings
                .Single(b => b.TrainingId == trainingId);
        }

        public bool InsertTraining(Training obj)
        {
            _context.Trainings.Add(obj);
            return _context.SaveChanges() > 0;

            //return _provider.ExecuteNonQuery($"CALL InsertTraining(" +
            //    $"'{obj.TraName}'," +
            //    $"'{obj.TraData}')");
        }
    }
}
