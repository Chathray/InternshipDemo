using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using BC = BCrypt.Net.BCrypt;

namespace WebApplication
{
    public class DataAdapter
    {
        private readonly DataContext _context;

        public DataAdapter(DataContext context)
        {
            _context = context;
        }

        internal DataContext GetContext()
        {
            return _context;
        }

        public User UserCheck(string eml, string pas)
        {
            if (string.IsNullOrEmpty(eml) || string.IsNullOrEmpty(pas))
                return null;

            //var user = _context.Users.SingleOrDefault(x => x.Email == email);

            var user = _context.Users
                .FromSqlRaw($"EXECUTE dbo.UserCheck '{eml}'")
                .ToList();

            // check if username exists
            if (user.Count < 1)
                return null;

            // check if password is correct
            if (!BC.Verify(pas, user[0].PasswordHash))
                return null;

            // authentication successful
            return user[0];
        }

        internal Intern CreateIntern(Intern model)
        {
            _context.Interns.Add(model);
            _context.SaveChanges();
            return model;
        }

        internal string InternLeave(int id)
        {
            var target = _context.Interns.FirstOrDefault(x => x.ID == id);
            _context.Interns.Remove(target);
            _context.SaveChanges();
            return target.Email;
        }

        internal IList<Intern> GetInterns()
        {
            return _context.Interns.ToList();
        }

        public IList<Question> GetQuestions()
        {
            return _context.Questions
                .OrderBy(y => y.Group)
                .ThenBy(n => n.ID)
                .ToList();
        }

        public User UserCreate(User user, string pas)
        {
            // validation
            if (string.IsNullOrWhiteSpace(pas))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Email == user.Email))
                throw new AppException("Email \"" + user.Email + "\" is already taken");

            user.PasswordHash = BC.HashPassword(pas);

            //_context.Users.Add(user);
            //_context.SaveChanges();

            var rowsAffected = _context.Database
                .ExecuteSqlRaw($"EXECUTE dbo.UserCreate " +
                $"'{user.FirstName}', " +
                $"'{user.LastName}', " +
                $"'{user.Email}', " +
                $"'{user.PasswordHash}'");
            return user;
        }

        internal Event CreateEvent(Event even)
        {
            _context.Events.Add(even);
            _context.SaveChanges();
            return even;
        }

        internal IList<EventType> GetEventTypes()
        {
            return _context.EventTypes.ToList();
        }
        internal IList<Event> GetEvents()
        {
            return _context.Events.ToList();
        }
        internal IList<User> GetUsers()
        {
            return _context.Users.ToList();
        }
    }
}