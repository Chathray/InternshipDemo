using System.ComponentModel.DataAnnotations;

namespace WebApplication
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        public string Group { get; set; }
        public string InData { get; set; }
        public string OutData { get; set; }
    }
}