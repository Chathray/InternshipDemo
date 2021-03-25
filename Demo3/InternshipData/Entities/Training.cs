using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internship.Data
{
    [Table("Trainings")]
    public class Training : EntityBase
    {
        [Key]
        public int TrainingId { get; set; }
        public string TraName { get; set; }
        public string TraData { get; set; }
    }
}