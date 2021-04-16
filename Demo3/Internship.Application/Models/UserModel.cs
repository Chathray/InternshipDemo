namespace Internship.Application
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }

        public string Status { get; set; } = "success";
        public string Role { get; set; } = "staff";
        public string Avatar { get; set; } = "/img/user.jpg";
        public string Phone { get; set; } = null;
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string HeaderPhoto { get; set; } = "/img/header.jpg";
    }
}