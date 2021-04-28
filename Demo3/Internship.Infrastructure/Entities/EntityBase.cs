using System.ComponentModel.DataAnnotations.Schema;

namespace Internship.Infrastructure
{
    public class EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string CreatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string UpdatedDate { get; set; }
    }
}