using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internship.Infrastructure
{
    [Table("InternshipPoints")]
    public class InternshipPoint:EntityBase
    {
        [Key]
        public int InternId { get; set; }
        public float TechnicalSkill { get; set; }
        public float SoftSkill { get; set; }
        public float Attitude { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public float Score { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool Passed { get; set; }

        [ForeignKey("InternId")]
        public Intern Interns { get; set; }
    }
}