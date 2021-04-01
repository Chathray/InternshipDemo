using System.Linq;

namespace Internship.Infrastructure
{
    public class PointRepository : Repository<Point>, IPointRepository
    {
        private readonly DataContext _context;
        private readonly DataProvider _provider;

        public PointRepository(DataContext context, DataProvider provider) : base(context)
        {
            _context = context;
            _provider = provider;
        }

        public bool EvaluateIntern(Point point)
        {
            return _provider
                          .ExecuteNonQuery($"CALL EvaluateIntern(" +
                          $"{point.InternId}," +
                          $"{point.Marker}," +
                          $"{point.TechnicalSkill}," +
                          $"{point.SoftSkill}," +
                          $"{point.Attitude})");
        }

        public Point GetPoint(int id)
        {
            var obj = _context.Points.SingleOrDefault(o => o.InternId == id);

            if (obj is null) return null;
            return obj;
        }

        public bool Delete(int id)
        {
            var obj = _context.Points.Single(o => o.InternId == id);
            _context.Remove(obj);
            return _context.SaveChanges() > 0;
        }

    }
}
