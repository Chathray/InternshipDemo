using Internship.Infrastructure;
using System.Collections.Generic;

namespace Internship.Application
{
    public class PointService : IPointService
    {
        private readonly IPointRepository _pointRespository;
        public PointService(IPointRepository pointRespository)
        {
            _pointRespository = pointRespository;
        }


        public IList<PointModel> GetAll()
        {
            var dep = _pointRespository.GetAll();
            var model = ObjectMapper.Mapper.Map<IList<Point>, IList<PointModel>>(dep);
            return model;
        }

        public bool EvaluateIntern(PointModel mark)
        {
            var point = ObjectMapper.Mapper.Map<Point>(mark);
            return _pointRespository.EvaluateIntern(point);
        }

        public PointModel GetPoint(int id)
        {
            var point = _pointRespository.GetPoint(id);
            return ObjectMapper.Mapper.Map<PointModel>(point);
        }

        public bool UpdatePoint(PointModel model)
        {
            var obj = ObjectMapper.Mapper.Map<Point>(model);
            return _pointRespository.Update(obj);
        }

        public bool DeletePoint(int id)
        {
            return _pointRespository.Delete(id);

        }
    }
}
