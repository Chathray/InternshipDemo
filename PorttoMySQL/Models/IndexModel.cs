using System.Collections.Generic;
using WebApplication.Helpers;

namespace WebApplication.Models
{
    public class IndexModel
    {
        private readonly DataAdapter _adapter;

        public IList<Intern> Internl { get; set; }
        public IList<Organization> Organizationl { get; set; }
        public IList<Department> Departmentl { get; set; }

        public IndexModel() { }
        public IndexModel(int id, int psize, DataAdapter ad, IList<Organization> or, IList<Department> dt)
        {
            _adapter = ad;

            Internl = _adapter.GetInternList(id, psize);

            Organizationl = or;
            Departmentl = dt;
        }

        public string GetFullName(int id)
        {
            return _adapter.GetFullName(id);
        }

        public int PageSize { get; set; }


        #region Intern Property
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedDate { get; set; }
        public string Duration { get; set; }
        public string Type { get; set; }
        public int Department { get; set; }
        public int Organization { get; set; }

        #endregion End Intern Property
    }
}
