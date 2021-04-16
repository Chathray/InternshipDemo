using System.Collections.Generic;

namespace Internship.Application
{
    public interface IUserService
    {
        IList<UserModel> GetAll();

        UserModel Authenticate(string loginEmail, string loginPassword);
        UserModel GetById(int userId);
        bool InsertUser(UserModel model);
        int CountByIndex(int stt);
    }
}