using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internship.Application
{
    public class EventTypeModel
    {
        public string Type { get; set; }
        public string ClassName { get; set; }
        public string Color { get; set; }
    }
}