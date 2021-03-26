namespace Internship.Data
{
    public interface IUserRepository : IRepository<User>
    {
        public User GetUser(string email, string password);
        public User GetById(int userId);
        public bool InsertUser(User user, string password);
    }
}
