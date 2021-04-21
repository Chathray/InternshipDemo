using System.Data;

namespace Internship.Infrastructure
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetUser(string email, string password);
        bool InsertUser(User user, string password);
        User GetById(int userId);
        DataTable GetView(int id);
        bool UpdatePassword(int userId, string hash);
        bool UpdateBasic(User user);
        bool UserDelete(int userId);
        bool SetStatus(int userId, string status);
    }
}
