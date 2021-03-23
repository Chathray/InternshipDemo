using Internship.Data;

namespace InternshipApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRespository _userRespository;
        public AccountService(IUserRespository userRespository)
        {
            _userRespository = userRespository;
        }

        public User Authenticate(string loginEmail, string loginPassword)
        {
            return _userRespository.GetUser(loginEmail, loginPassword);
        }

        public User GetById(int userId)
        {
            return _userRespository.GetById(userId);
        }

        public bool InsertUser(User user, string regiterPassword)
        {
            return _userRespository.InsertUser(user, regiterPassword);
        }
    }
}
