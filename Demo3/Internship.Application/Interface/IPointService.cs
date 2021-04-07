using System.Collections.Generic;

namespace Internship.Application
{
    public interface IPointService
    {
        IList<PointModel> GetAll();

        bool EvaluateIntern(PointModel mark);
        PointModel GetPoint(int id);
        bool UpdatePoint(PointModel model);
        bool DeletePoint(int id);
        IList<PointListModel> GetAllWithName();
        int GetPassedCount();
    }
}