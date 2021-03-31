using System.Collections.Generic;
using System.Threading.Tasks;

namespace Internship.Application
{
    public interface IUserService
    {
        Task<IReadOnlyList<UserModel>> GetAllAsync();
        Task<int> GetCountAsync();

        UserModel Authenticate(string loginEmail, string loginPassword);
        UserModel GetById(int userId);
        bool InsertUser(UserModel model);
    }
}