using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebApplication.Data;
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

        public bool InsertIntern(Intern model)
        {
            return DataProvider.ExecuteNonQuery($"CALL InsertIntern(" +
             $"'{model.Email}', " +
             $"'{model.Phone}', " +
             $"'{model.FirstName}', " +
             $"'{model.LastName}', " +
             $"'{model.DateOfBirth}', " +
             $"'{model.Gender}', " +
             $"'{model.Duration}', " +
             $"'{model.Type}', " +
             $"'{model.Mentor}', " +
             $"'{model.TrainingId}', " +
             $"'{model.OrganizationId}', " +
             $"'{model.DepartmentId}')");
        }

        public bool InsertUser(User user, string pas)
        {
            // validation
            if (string.IsNullOrWhiteSpace(pas))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Email == user.Email))
                throw new AppException("Email \"" + user.Email + "\" is already taken");

            user.PasswordHash = BC.HashPassword(pas);

            return DataProvider.ExecuteNonQuery($"CALL InsertUser(" +
                $"'{user.FirstName}', " +
                $"'{user.LastName}', " +
                $"'{user.Email}', " +
                $"'{user.PasswordHash}')");
        }

        public bool InsertEvent(Event even)
        {
            //_context.Events.Add(even);
            //_context.SaveChanges();

            return DataProvider.ExecuteNonQuery($"CALL InsertEvent(" +
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
        }

        public string RemoveIntern(int id)
        {
            var target = _context.Interns.FirstOrDefault(x => x.InternId == id);
            _context.Interns.Remove(target);
            _context.SaveChanges();
            return target.Email;
        }

        public bool UpdateIntern(Intern model)
        {
            return DataProvider.ExecuteNonQuery($"CALL UpdateIntern(" +
             $"'{model.InternId}', " +
             $"'{model.Email}', " +
             $"'{model.Phone}', " +
             $"'{model.FirstName}', " +
             $"'{model.LastName}', " +
             $"'{model.DateOfBirth}', " +
             $"'{model.Gender}', " +
             $"'{model.Duration}', " +
             $"'{model.Type}', " +
             $"'{model.Mentor}', " +
             $"'{model.TrainingId}', " +
             $"'{model.OrganizationId}', " +
             $"'{model.DepartmentId}', " +
             $"'{model.Avatar}')");
        }


        /// GET STATE
        public IList<Question> GetQuestions()
        {
            return _context.Questions
                .OrderBy(y => y.Group)
                .ThenBy(n => n.QuestionId)
                .ToList();
        }

        public IList<Intern> GetInternList(int page, int size)
        {
            return _context.Interns
                     .Include(b => b.Organizations)
                     .Include(b => b.Departments)
                     .Include(b => b.Users)
                     .Include(b => b.Trainings)
                     .OrderBy(i => i.InternId)
                     .ThenBy(i => i.FirstName)
                     .Skip((page - 1) * size)
                     .Take(size)
                     .ToList();
        }


        public User GetUser(string eml, string pas)
        {
            if (string.IsNullOrEmpty(eml) || string.IsNullOrEmpty(pas))
                return null;

            //var user = _context.Users.SingleOrDefault(x => x.Email == eml);

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

        public IList<User> GetUsers()
        {
            return _context.Users.ToList();
        }
        public IList<Event> GetEvents()
        {
            return _context.Events.ToList();
        }
        public IList<EventType> GetEventTypes()
        {
            return _context.EventTypes.ToList();
        }
        public IList<Intern> GetInterns()
        {
            return _context.Interns.ToList();
        }
        public IList<Training> GetTrainings()
        {
            return _context.Trainings.ToList();
        }
        public IList<Organization> GetOrganizations()
        {
            return _context.Organizations.ToList();
        }

        public string GetInternInfo(int id)
        {
            return DataProvider.ExecuteScalar($"CALL GetInternInfo('{id}')").ToString();
        }

        public IList<Department> GetDepartments()
        {
            return _context.Departments.ToList();
        }
        public int GetInternCount()
        {
            return _context.Interns.Count();
        }

        public string GetInternTraining(int id)
        {
            var data = DataProvider.ExecuteReader($"CALL GetTrainingData('{id}')");
            return data.Rows[0]["json"].ToString();
        }

        public DataTable GetEventsIntern()
        {
            return DataProvider.ExecuteReader($"CALL GetEventsJoined()");
        }

        public string GetEventsJson()
        {
            var data = DataProvider.ExecuteReader($"CALL GetEventsJson()");
            return data.Rows[0]["json"].ToString();
        }

        public DataTable GetInternModelList(int page, int size, string sort)
        {
            return DataProvider.ExecuteReader(
                $"CALL GetInternList({(page - 1) * size},{size},'{sort}')");
        }
    }
}