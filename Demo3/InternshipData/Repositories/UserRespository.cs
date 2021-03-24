using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Internship.Data
{
    public class UserRespository : Repository<User>, IUserRespository
    {
        private readonly DataContext _context;
        private readonly DataProvider _provider;

        public UserRespository(DataContext context, DataProvider provider) : base(context)
        {
            _context = context;
            _provider = provider;
        }

        Task<IReadOnlyList<User>> IUserRespository.GetAllAsync()
        {
            return GetAllAsync();
        }

        public User GetById(int userId)
        {
            return _context.Users.SingleOrDefault(x => x.UserId == userId);
        }

        public User GetUser(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Email == email);

            // check if username exists
            if (user is null)
                return null;

            // check if password is correct
            if (!BC.Verify(password, user.PasswordHash))
                return null;

            // authentication successful
            return user;
        }

        public bool InsertUser(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (_context.Users.Any(x => x.Email == user.Email))
                return false;

            user.PasswordHash = BC.HashPassword(password);

            return _provider.ExecuteNonQuery($"CALL InsertUser(" +
                $"'{user.FirstName}', " +
                $"'{user.LastName}', " +
                $"'{user.Email}', " +
                $"'{user.PasswordHash}')");
        }
    }
}