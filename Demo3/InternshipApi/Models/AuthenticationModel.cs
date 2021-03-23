using System.ComponentModel.DataAnnotations;

namespace InternshipApi.Models
{
    public class AuthenticationModel
    {
        public string LoginEmail { get; set; }

        public string LoginPassword { get; set; }

        /*-----------------------------------*/

        [Required]
        public string ResetEmail { get; set; }

        /*-----------------------------------*/

        [Required]
        public string RegiterFirstName { get; set; }

        [Required]
        public string RegiterLastName { get; set; }

        [Required]
        public string RegiterEmail { get; set; }

        [Required]
        public string RegiterPassword { get; set; }
    }
}
