using Internship.Infrastructure;
using System.Collections.Generic;

namespace Internship.Application
{
    public class InternshipPointService : IInternshipPointService
    {
        private readonly IInternshipPointRepository _internshipPointRespository;
        public InternshipPointService(IInternshipPointRepository internshipPointRespository)
        {
            _internshipPointRespository = internshipPointRespository;
        }

        public int GetCount()
        {
            return _internshipPointRespository.GetCount();
        }

        public IList<InternshipPointModel> GetAll()
        {
            var dep = _internshipPointRespository.GetAll();
            var model = ObjectMapper.Mapper.Map<IList<InternshipPoint>, IList<InternshipPointModel>>(dep);
            return model;
        }

        public bool EvaluateIntern(InternshipPointModel mark)
        {
            var point = ObjectMapper.Mapper.Map<InternshipPoint>(mark);
            return _internshipPointRespository.EvaluateIntern(point);
        }

        public InternshipPointModel GetPoint(int id)
        {
            var point = _internshipPointRespository.GetPoint(id);
            return ObjectMapper.Mapper.Map<InternshipPointModel>(point);
        }

        public bool UpdatePoint(InternshipPointModel model)
        {
            var obj = ObjectMapper.Mapper.Map<InternshipPoint>(model);
            return _internshipPointRespository.Update(obj);
        }

        public bool DeletePoint(int id)
        {
            return _internshipPointRespository.Delete(id);

        }
    }
}
