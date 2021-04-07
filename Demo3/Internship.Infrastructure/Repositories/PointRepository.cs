using Microsoft.EntityFrameworkCore;
using System.Linq;
using Dapper;
using System.Data;
using System;

namespace Internship.Infrastructure
{
    public class PointRepository : Repository<Point>, IPointRepository
    {
        private readonly DataContext _context;


        public PointRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public bool EvaluateIntern(Point point)
        {
            return _context.Database.GetDbConnection()
                          .Execute($"CALL EvaluateIntern(" +
                          $"{point.InternId}," +
                          $"{point.Marker}," +
                          $"{point.TechnicalSkill}," +
                          $"{point.SoftSkill}," +
                          $"{point.Attitude})") > 0;
        }

        public Point GetPoint(int id)
        {
            var obj = _context.Points.SingleOrDefault(o => o.InternId == id);

            if (obj is null) return null;
            return obj;
        }

        public IDataReader GetAllWithName()
        {            
            return _context.Database.GetDbConnection()
                 .ExecuteReader($"CALL GetAllPointWithName()");
        }

        public int GetPassedCount()
        {
            return Convert.ToInt32(_context.Database.GetDbConnection()
                 .ExecuteScalar($"CALL HowManyPassed()"));
        }
    }
}
