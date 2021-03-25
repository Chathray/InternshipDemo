using Internship.Data;
using System.Collections.Generic;

namespace InternshipApi.Models
{
    public class InternshipModel
    {
        public PaginationLogic Pager { get; set; }

        public IList<InternInfoModel> Interns { get; set; }
        public IReadOnlyList<Organization> Organizations { get; set; }
        public IReadOnlyList<Department> Departments { get; set; }
        public IReadOnlyList<Training> Trainings { get; set; }

        public InternshipModel() { }
        public InternshipModel(PaginationLogic pager, IList<InternInfoModel> interns, IReadOnlyList<Training> trainings, IReadOnlyList<Organization> organizations, IReadOnlyList<Department> departments)
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
