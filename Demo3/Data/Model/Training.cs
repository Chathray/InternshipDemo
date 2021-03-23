using System.ComponentModel.DataAnnotations;

namespace WebApplication
{
    public class Training
    {
        [Key]
        public int TrainingId { get; set; }
        public string TraName { get; set; }
        public string TraData { get; set; }
    }
}