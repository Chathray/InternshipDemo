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
    }
}
