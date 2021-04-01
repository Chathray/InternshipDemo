using Internship.Application;
using System.Collections.Generic;
using System.Data;

namespace Internship.Web
{
    public class IndexViewModel
    {
        public PaginationLogic Pager { get; set; }

        public DataSet Interns { get; set; }
        public IList<OrganizationModel> Organizations { get; set; }
        public IList<DepartmentModel> Departments { get; set; }
        public IList<TrainingModel> Trainings { get; set; }
        public IList<PointModel> Points { get; set; }

        public IndexViewModel() { }
        public IndexViewModel(PaginationLogic pager, DataSet interns, IList<TrainingModel> trainings, IList<OrganizationModel> organizations, IList<DepartmentModel> departments, IList<PointModel> points)
        {
            Pager = pager;
            Interns = interns;
            Trainings = trainings;
            Organizations = organizations;
            Departments = departments;
            Points = points;
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


        public string CheckPageActive(int page)
        {
            return page == Pager.CurrentPage ? "active" : "";
        }
    }
}