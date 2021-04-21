using Internship.Infrastructure;
using System.Data;

namespace Internship.Application
{
    public class UserService : ServiceBase<UserModel, User>, IUserService
    {
        private readonly IUserRepository _userRespository;

        public UserService(IUserRepository userRespository) : base(userRespository)
        {
            _userRespository = userRespository;
        }

        public UserModel Authenticate(string loginEmail, string loginPassword)
        {
            var user = _userRespository.GetUser(loginEmail, loginPassword);
            var model = ObjectMapper.Mapper.Map<UserModel>(user);
            return model;
        }

        public DataTable GetView(int id)
        {
            return _userRespository.GetView(id);
        }

        public bool InsertUser(UserModel model)
        {
            var user = ObjectMapper.Mapper.Map<User>(model);
            return _userRespository.InsertUser(user, model.Password);
        }

        public bool SetStatus(int userId, string status)
        {
            return _userRespository.SetStatus(userId, status);
        }

        public bool UpdateBasic(UserModel model)
        {
            var user = ObjectMapper.Mapper.Map<User>(model);
            return _userRespository.UpdateBasic(user);
        }

        public bool UpdatePassword(int userId, string newPassword)
        {
            return _userRespository.UpdatePassword(userId, newPassword);
        }

        public bool UserDelete(int userId)
        {
            return _userRespository.UserDelete(userId);
        }
    }
}
