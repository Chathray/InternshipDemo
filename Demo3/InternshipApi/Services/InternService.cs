using Internship.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternshipApi.Services
{
    public class InternService : IInternService
    {
        private readonly IInternRepository _internRespository;
        public InternService(IInternRepository internRespository)
        {
            _internRespository = internRespository;
        }

        public Task<IReadOnlyList<Intern>> GetAllAsync()
        {
            return _internRespository.GetAllAsync();
        }

        public Task<int> GetCountAsync()
        {
            return _internRespository.GetCountAsync();
        }

        public IList<Intern> GetInternByPage(int page, int size)
        {
            return _internRespository.GetInternByPage(page, size);
        }

        public IList<InternViewModel> GetInternByPage(int page, int size, string sort)
        {
            var dt = _internRespository.GetInternByPage(page, size, sort);

            //return dt.AsEnumerable().Select(row =>
            //    new InternshipModel
            //    {
            //        Avatar = row.Field<string>("Avatar"),
            //        DateOfBirth = row.Field<string>("DateOfBirth"),
            //        Department = row.Field<string>("DepName"),
            //        Duration = row.Field<string>("Duration"),
            //        Email = row.Field<string>("Email"),
            //        FullName = row.Field<string>("FullName"),
            //        Gender = row.Field<string>("Gender"),
            //        Organization = row.Field<string>("OrgName"),
            //        Type = row.Field<string>("Type"),0
            //        Training = row.Field<string>("TraName")
            //    }).ToList();

            return DataExtensions.ConvertDataTable<InternViewModel>(dt);
        }

        public string GetInternInfo(int id)
        {
            return _internRespository.GetInternInfo(id);
        }

        public bool InsertIntern(Intern model)
        {
            return _internRespository.InsertIntern(model);
        }

        public bool RemoveIntern(int id)
        {
            return _internRespository.RemoveIntern(id);
        }

        public bool UpdateIntern(Intern model)
        {
            return _internRespository.UpdateIntern(model);
        }
    }
}
