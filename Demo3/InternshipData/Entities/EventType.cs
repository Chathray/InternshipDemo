using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internship.Data
{
    [Table("EventTypes")]
    public class EventType
    {
        [Key]
        public string Type { get; set; }
        public string ClassName { get; set; }
        public string Color { get; set; }
    }
}