using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internship.Application
{
    public class TrainingModel 
    {
        public int TrainingId { get; set; }
        public string TraName { get; set; }
        public string TraData { get; set; }
    }
}