namespace Internship.Infrastructure
{
    public interface IPointRepository : IRepository<Point>
    {
        bool EvaluateIntern(Point point);
        Point GetPoint(int id);
        bool Delete(int id);

    }
}
