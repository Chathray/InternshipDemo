using System.ComponentModel.DataAnnotations;

namespace WebApplication
{
    public class Organization
    {
        [Key]
        public int OrganizationId { get; set; }
        public string OrgName { get; set; }
        public string OrgAddress { get; set; }
        public string OrgPhone { get; set; }
    }
}