using System.ComponentModel.DataAnnotations.Schema;

namespace Internship.Infrastructure
{
    public class EntityBase
    {
        [NotMapped]
        public string CreatedDate { get; set; }
        [NotMapped]
        public string UpdatedDate { get; set; }
    }
}