using Internship.Data;

namespace InternshipApi.Services
{
    public interface IAccountService
    {
        User Authenticate(string loginEmail, string loginPassword);
        User GetById(int userId);
        bool InsertUser(User user, string regiterPassword);
    }
}