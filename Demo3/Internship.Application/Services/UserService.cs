using AutoMapper;
using Internship.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public Task<IReadOnlyList<UserModel>> GetAllAsync()
        {
            var user = _userRespository.GetAllAsync();
            var model = ObjectMapper.Mapper.Map<Task<IReadOnlyList<UserModel>>>(user);

            return model;
        }

        public Task<int> GetCountAsync()
        {
            return _userRespository.GetCountAsync();
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
    }
}
