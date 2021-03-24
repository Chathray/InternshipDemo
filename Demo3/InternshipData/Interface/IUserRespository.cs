using System.Collections.Generic;
using System.Threading.Tasks;

namespace Internship.Data
{
    public interface IUserRespository
    {
        public Task<IReadOnlyList<User>> GetAllAsync();
        public User GetUser(string email, string password);
        public User GetById(int userId);
        public bool InsertUser(User user, string password);
    }
}
