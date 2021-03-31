using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internship.Infrastructure
{
    [Table("Departments")]
    public class Department : EntityBase
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepName { get; set; }
        public string DepLocation { get; set; }
    }
}