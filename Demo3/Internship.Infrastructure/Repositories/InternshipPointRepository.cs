using System.Linq;

namespace Internship.Infrastructure
{
    public class InternshipPointRepository : Repository<InternshipPoint>, IInternshipPointRepository
    {
        private readonly DataContext _context;
        private readonly DataProvider _provider;

        public InternshipPointRepository(DataContext context, DataProvider provider) : base(context)
        {
            _context = context;
            _provider = provider;
        }

        public bool EvaluateIntern(InternshipPoint point)
        {
            return _provider
                          .ExecuteNonQuery($"CALL EvaluateIntern(" +
                          $"{point.InternId}," +
                          $"{point.TechnicalSkill}," +
                          $"{point.SoftSkill}," +
                          $"{point.Attitude})");
        }

        public InternshipPoint GetPoint(int id)
        {
            var obj = _context.InternshipPoints.SingleOrDefault(o => o.InternId == id);

            if (obj is null) return null;
            return obj;
        }

        public bool Update(InternshipPoint obj)
        {
            _context.InternshipPoints.Update(obj);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var obj = _context.InternshipPoints.Single(o => o.InternId == id);
            _context.Remove(obj);
            return _context.SaveChanges() > 0;
        }

    }
}
