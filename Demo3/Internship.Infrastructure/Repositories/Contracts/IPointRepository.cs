using System.Data;

namespace Internship.Infrastructure
{
    public interface IPointRepository : IRepositoryBase<Point>
    {
        bool EvaluateIntern(Point point);
        Point GetPoint(int id);
        IDataReader GetAllWithName();
        int GetPassedCount();
    }
}
