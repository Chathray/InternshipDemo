namespace Internship.Data
{
    public interface IUserRespository
    {
        public User GetUser(string email, string password);
        public bool InsertUser(User user, string password);
        public User GetById(int userId);
    }
}
