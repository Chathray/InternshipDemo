using Internship.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Internship.Application
{
    public class InternshipPointService : IInternshipPointService
    {
        private readonly IInternshipPointRepository _internshipPointRespository;
        public InternshipPointService(IInternshipPointRepository internshipPointRespository)
        {
            _internshipPointRespository = internshipPointRespository;
        }

        public Task<int> GetCountAsync()
        {
            return _internshipPointRespository.GetCountAsync();
        }

        public Task<IReadOnlyList<InternshipPointModel>> GetAllAsync()
        {
            var dep = _internshipPointRespository.GetAllAsync();
            var model = ObjectMapper.Mapper.Map<Task<IReadOnlyList<InternshipPointModel>>>(dep);
            return model;
        }
    }
}
