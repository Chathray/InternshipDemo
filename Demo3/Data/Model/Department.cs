using System.ComponentModel.DataAnnotations;

namespace WebApplication
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepName { get; set; }
        public string DepLocation { get; set; }
    }
}