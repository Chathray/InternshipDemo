using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class AuthenticationModel
    {
        [Required]
        public string LoginMail { get; set; }

        [Required]
        public string LoginPass { get; set; }

        /*-----------------------------------*/

        [Required]
        public string ResetEmail { get; set; }

        /*-----------------------------------*/

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
