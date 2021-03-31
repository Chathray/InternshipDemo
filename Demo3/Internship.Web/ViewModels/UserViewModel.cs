using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internship.Web
{
    public class UserViewModel
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
