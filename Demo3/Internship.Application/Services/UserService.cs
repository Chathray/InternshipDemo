using Internship.Infrastructure;
using System.Collections.Generic;

namespace Internship.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRespository;

        public UserService(IUserRepository userRespository)
        {
            _userRespository = userRespository;
        }

        public UserModel Authenticate(string loginEmail, string loginPassword)
        {
            var user = _userRespository.GetUser(loginEmail, loginPassword);
            var model = ObjectMapper.Mapper.Map<UserModel>(user);
            return model;
        }

        public IList<UserModel> GetAll()
        {
            var user = _userRespository.GetAll();
            var model = ObjectMapper.Mapper.Map<IList<User>, IList<UserModel>>(user);

            return model;
        }

        public UserModel GetById(int userId)
        {
            var user = _userRespository.GetById(userId);
            return ObjectMapper.Mapper.Map<UserModel>(user);
        }

        public bool InsertUser(UserModel model)
        {
            var user = ObjectMapper.Mapper.Map<User>(model);
            return _userRespository.InsertUser(user, model.Password);
        }

        public int CountByIndex(int stt)
        {
            return _userRespository.CountByIndex(stt);
        }
    }
}
