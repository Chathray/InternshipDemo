using Internship.Data;
using InternshipApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternshipApi.Services
{
    public interface IUserService
    {
        Task<IReadOnlyList<User>> GetAllAsync();
        Task<int> GetCountAsync();

        User Authenticate(string loginEmail, string loginPassword);
        User GetById(int userId);
        bool InsertUser(AuthenticationModel model);
    }
}