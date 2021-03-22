using System.Collections.Generic;
using System.Data;
using WebApplication.Helpers;

namespace WebApplication.Models
{
    public class IndexModel
    {
        public PaginationLogic Pager { get; set; }

        public DataTable Internl { get; set; }
        public IList<Organization> Organizationl { get; set; }
        public IList<Department> Departmentl { get; set; }
        public IList<Training> Trainingl { get; set; }

        public IndexModel() { }
        public IndexModel(PaginationLogic pg, DataTable it, IList<Training> tr, IList<Organization> or, IList<Department> dt)
        {
            Pager = pg;
            Internl = it;
            Trainingl = tr;
            Organizationl = or;
            Departmentl = dt;
        }

        public string CheckActive(int i)
        {
            return i == Pager.CurrentPage ? "active" : "";
        }

        #region Intern Property
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Duration { get; set; }
        public string Type { get; set; }
        public int DepartmentId { get; set; }
        public int OrganizationId { get; set; }
        public int TrainingId { get; set; }

        #endregion End Intern Property
    }
}
