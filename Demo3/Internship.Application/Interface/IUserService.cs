using System.Collections.Generic;

namespace Internship.Application
{
    public interface IUserService
    {
        IList<UserModel> GetAll();
        int GetCount();

        UserModel Authenticate(string loginEmail, string loginPassword);
        UserModel GetById(int userId);
        bool InsertUser(UserModel model);
    }
}