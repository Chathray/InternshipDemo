using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BC = BCrypt.Net.BCrypt;

namespace Internship.Infrastructure
{
    public class UserRespository : Repository<User>, IUserRepository
    {
        private readonly DataContext _context;

        public UserRespository(DataContext context) : base(context)
        {
            _context = context;
        }

        public User GetById(int userId)
        {
            return _context.Users.SingleOrDefault(x => x.UserId == userId);
        }

        public User GetUser(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Database.GetDbConnection()
                .QuerySingle<User>($"SELECT * FROM users WHERE email = '{email}'");

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

            return _context.Database.GetDbConnection()
                .Execute($@"CALL InsertUser(
                '{user.Email}', 
                '{user.FirstName}', 
                '{user.LastName}', 
                '{user.PasswordHash}')") > 0;
        }
    }
}