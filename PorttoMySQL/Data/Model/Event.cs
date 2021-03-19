using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string ClassName { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedDate { get; set; }
        //[NotMapped]
        public string GestsField { get; set; }

        public string RepeatField { get; set; }
        public string EventLocationLabel { get; set; }
        public string EventDescriptionLabel { get; set; }
        public string Image { get; set; }
    }
}