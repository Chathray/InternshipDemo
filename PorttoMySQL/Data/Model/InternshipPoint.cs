using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication
{
    public class InternshipPoint
    {
        [Key]
        public int InternId { get; set; }
        public float TechnicalSkill { get; set; }
        public float SoftSkill { get; set; }
        public float Attitude { get; set; }
        public float Result { get; set; }

        [ForeignKey("InternId")]
        public Intern Interns { get; set; }
    }
}