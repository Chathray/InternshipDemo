using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internship.Data
{
    [Table("Users")]
    public class User : EntityBase
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
        public string Avatar { get; set; }
        public string Phone { get; set; }
    }
}