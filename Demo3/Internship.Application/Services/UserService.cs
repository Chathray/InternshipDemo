using Internship.Infrastructure;

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
    }
}
