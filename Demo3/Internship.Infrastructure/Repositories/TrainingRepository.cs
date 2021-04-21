using System.Linq;

namespace Internship.Infrastructure
{
    public class TrainingRepository : RepositoryBase<Training>, ITrainingRepository
    {
        private readonly DataContext _context;


        public TrainingRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public Training GetTrainingByIntern(int id)
        {
            var trainingId = _context.Interns
                .Single(b => b.InternId == id).TrainingId;

            return _context.Trainings
                .Single(b => b.TrainingId == trainingId);
        }

        public string GetTrainingContent(int id)
        {
            var obj = _context.Trainings.SingleOrDefault(o => o.TrainingId == id);
            return obj.TraData;
        }
    }
}
