using Internship.Application;
using System.Collections.Generic;
using System.Data;

namespace Internship.Web
{
    public class IndexViewModel
    {
        public PaginationLogic Pager { get; set; }

        public DataSet Interns { get; set; }
        public IReadOnlyList<OrganizationModel> Organizations { get; set; }
        public IReadOnlyList<DepartmentModel> Departments { get; set; }
        public IReadOnlyList<TrainingModel> Trainings { get; set; }
        public IReadOnlyList<InternshipPointModel> InternshipPoints { get; set; }

        public IndexViewModel() { }
        public IndexViewModel(PaginationLogic pager, DataSet interns, IReadOnlyList<TrainingModel> trainings, IReadOnlyList<OrganizationModel> organizations, IReadOnlyList<DepartmentModel> departments, IReadOnlyList<InternshipPointModel> internshippoints)
        {
            Pager = pager;
            Interns = interns;
            Trainings = trainings;
            Organizations = organizations;
            Departments = departments;
            InternshipPoints = internshippoints;
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