using System.Collections.Generic;
using System.Data;
using WebApplication.Helpers;

namespace WebApplication.Models
{
    public class IndexModel
    {
        public PaginationLogic Pager { get; set; }

        public DataTable Interns { get; set; }
        public IList<Organization> Organizations { get; set; }
        public IList<Department> Departments { get; set; }
        public IList<Training> Trainings { get; set; }

        public IndexModel() { }
        public IndexModel(PaginationLogic pager, DataTable interns, IList<Training> trainings, IList<Organization> organizations, IList<Department> departments)
        {
            Pager = pager;
            Interns = interns;
            Trainings = trainings;
            Organizations = organizations;
            Departments = departments;
        }

        public string CheckPageActive(int page)
        {
            return page == Pager.CurrentPage ? "active" : "";
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
