using Internship.Infrastructure;
using System.Collections.Generic;
using System.Data;

namespace Internship.Application
{
    public class InternService : IInternService
    {
        private readonly IInternRepository _internRespository;
        public InternService(IInternRepository internRespository)
        {
            _internRespository = internRespository;
        }

        public IList<InternModel> GetAll()
        {
            var obj = _internRespository.GetAll();
            return ObjectMapper.Mapper.Map<IList<Intern>, IList<InternModel>>(obj);
        }


        public DataSet GetInternByPage(int currentPage, int pageSize, int sort, int search_on, string search_string)
        {
            return _internRespository.GetInternModelList(currentPage, pageSize, sort, search_on, search_string);
        }

        public IList<InternListModel> GetInternByPage(int page, int size, string sort)
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

            return DataExtensions.ConvertDataTable<InternListModel>(dt);
        }

        public IList<InternModel> GetInternByPage(int page, int size)
        {
            var obj = _internRespository.GetInternByPage(page, size);
            return ObjectMapper.Mapper.Map<IList<InternModel>>(obj);
        }

        public string GetInternInfo(int id)
        {
            return _internRespository.GetInternInfo(id);
        }

        public string GetInternDetail(int id)
        {
            return _internRespository.GetInternDetail(id);
        }
        public bool DeleteIntern(int id)
        {
            return _internRespository.RemoveIntern(id);
        }

        public bool InsertIntern(InternModel model)
        {
            var intern = ObjectMapper.Mapper.Map<Intern>(model);
            return _internRespository.InsertIntern(intern);
        }

        public bool UpdateIntern(InternModel model)
        {
            var intern = ObjectMapper.Mapper.Map<Intern>(model);
            return _internRespository.UpdateIntern(intern);
        }
    }
}
