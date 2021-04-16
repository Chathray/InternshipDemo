namespace Internship.Infrastructure
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetUser(string email, string password);
        User GetById(int userId);
        bool InsertUser(User user, string password);
    }
}
