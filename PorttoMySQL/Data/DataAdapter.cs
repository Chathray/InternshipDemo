using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Models;
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

        public DataContext GetContext()
        {
            return _context;
        }

        public string GetFullName(int id)
        {
            var u = _context.Users.SingleOrDefault(x => x.UserId == id);
            return u.FirstName + " " + u.LastName;
        }

        public User UserCheck(string eml, string pas)
        {
            if (string.IsNullOrEmpty(eml) || string.IsNullOrEmpty(pas))
                return null;

            //var user = _context.Users.SingleOrDefault(x => x.Email == email);

            var user = _context.Users
                .FromSqlRaw($"CALL CheckUser('{eml}')")
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

        public IList<Intern> GetInternList(int page, int size)
        {
            if (page < 1) page = 1;
            if (size < 2) size = 2;

            return _context.Interns
                     .Include(b => b.Or)
                     .Include(b => b.De)
                     .Include(b => b.Us)
                     .Skip((page - 1) * size)
                     .Take(size)
                     .ToList();
        }

        public int CreateIntern(Intern model)
        {
            var rowsAffected = _context.Database
            .ExecuteSqlRaw($"CALL CreateIntern(" +
            $"'{model.Email}', " +
            $"'{model.Phone}', " +
            $"'{model.FirstName}', " +
            $"'{model.LastName}', " +
            $"'{model.DateOfBirth}', " +
            $"'{model.Gender}', " +
            $"'{model.Duration}', " +
            $"'{model.Type}', " +
            $"'{model.Mentor}', " +
            $"'{model.Organization}', " +
            $"'{model.Department}')");

            return rowsAffected;
        }

        public string InternLeave(int id)
        {
            var target = _context.Interns.FirstOrDefault(x => x.InternId == id);
            _context.Interns.Remove(target);
            _context.SaveChanges();
            return target.Email;
        }


        public IList<Question> GetQuestions()
        {
            return _context.Questions
                .OrderBy(y => y.Group)
                .ThenBy(n => n.QuestionId)
                .ToList();
        }

        public int UserCreate(User user, string pas)
        {
            // validation
            if (string.IsNullOrWhiteSpace(pas))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Email == user.Email))
                throw new AppException("Email \"" + user.Email + "\" is already taken");

            user.PasswordHash = BC.HashPassword(pas);

            var rowsAffected = _context.Database
                .ExecuteSqlRaw($"CALL CreateUser(" +
                $"'{user.FirstName}', " +
                $"'{user.LastName}', " +
                $"'{user.Email}', " +
                $"'{user.PasswordHash}')");

            return rowsAffected;
        }

        public int CreateEvent(Event even)
        {
            //_context.Events.Add(even);
            //_context.SaveChanges();

            var affected = _context.Database
                .ExecuteSqlRaw($"CALL CreateEvent(" +
                $"'{even.Title}', " +
                $"'{even.Type}', " +
                $"'{even.ClassName}', " +
                $"'{even.Start}', " +
                $"'{even.End}', " +
                $"'{even.CreatedBy}', " +
                $"'{even.GestsField}', " +
                $"'{even.RepeatField}', " +
                $"'{even.EventLocationLabel}', " +
                $"'{even.EventDescriptionLabel}')");

            return affected;
        }


        /// <summary>
        /// GET STATE
        /// </summary>
        /// <returns></returns>
        public IList<EventType> GetEventTypes()
        {
            return _context.EventTypes.ToList();
        }
        public IList<Event> GetEvents()
        {
            return _context.Events.ToList();
        }
        public IList<Intern> GetInterns()
        {
            return _context.Interns.ToList();
        }
        public IList<User> GetUsers()
        {
            return _context.Users.ToList();
        }
        public IList<Organization> GetOrganizations()
        {
            return _context.Organizations.ToList();
        }
        public IList<Department> GetDepartments()
        {
            return _context.Departments.ToList();
        }
    }
}