using Internship.Infrastructure;
using System.Collections.Generic;
using System.Data;

namespace Internship.Application
{
    public class PointService : ServiceBase<PointModel, Point>, IPointService
    {
        private readonly IPointRepository _pointRespository;
        public PointService(IPointRepository pointRespository) : base(pointRespository)
        {
            _pointRespository = pointRespository;
        }



        public bool EvaluateIntern(PointModel mark)
        {
            var point = ObjectMapper.Mapper.Map<Point>(mark);
            return _pointRespository.EvaluateIntern(point);
        }


        public IList<PointListModel> GetAllWithName()
        {
            DataTable table = new();
            table.Load(_pointRespository.GetAllWithName());

            return DataExtensions.ConvertDataTable<PointListModel>(table);
        }

        public int GetPassedCount()
        {
            return _pointRespository.GetPassedCount();
        }
    }
}
