using System.ComponentModel.DataAnnotations;

namespace WebApplication
{
    public class InternshipPoint
    {
        [Key]
        public string InternId { get; set; }
        public float TechnicalSkill { get; set; }
        public float SoftSkill { get; set; }
        public float Attitude { get; set; }
        public float Result { get; set; }
    }
}