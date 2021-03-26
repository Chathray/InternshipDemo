using AutoMapper;
using Internship.Data;
using InternshipApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternshipApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRespository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRespository, IMapper mapper)
        {
            _userRespository = userRespository;
            _mapper = mapper;
        }

        public User Authenticate(string loginEmail, string loginPassword)
        {
            return _userRespository.GetUser(loginEmail, loginPassword);
        }

        public Task<IReadOnlyList<User>> GetAllAsync()
        {
            return _userRespository.GetAllAsync();
        }

        public Task<int> GetCountAsync()
        {
            return _userRespository.GetCountAsync();
        }

        public User GetById(int userId)
        {
            return _userRespository.GetById(userId);
        }

        public bool InsertUser(AuthenticationModel model)
        {
            // map model to entity
            var user = _mapper.Map<User>(model);

            return _userRespository.InsertUser(user, model.RegiterPassword);
        }
    }
}
