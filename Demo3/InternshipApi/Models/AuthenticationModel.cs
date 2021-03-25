namespace InternshipApi.Models
{
    public class AuthenticationModel
    {
        public string LoginEmail { get; set; }

        public string LoginPassword { get; set; }

        /*-----------------------------------*/

        public string ResetEmail { get; set; }

        /*-----------------------------------*/

        public string RegiterFirstName { get; set; }

        public string RegiterLastName { get; set; }

        public string RegiterEmail { get; set; }

        public string RegiterPassword { get; set; }
    }
}
