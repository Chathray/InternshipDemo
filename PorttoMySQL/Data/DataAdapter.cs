using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebApplication.Data;
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

        public string RemoveIntern(int id)
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

        public IList<Intern> GetInternList(int page, int size)
        {
            return _context.Interns
                     .Include(b => b.Or)
                     .Include(b => b.De)
                     .Include(b => b.Us)
                     .Include(b => b.Tr)
                     .OrderBy(i => i.InternId)
                     .ThenBy(i => i.FirstName)
                     .Skip((page - 1) * size)
                     .Take(size)
                     .ToList();
        }

        public DataSet GetInternModelList(int page, int size)
        {
            return DataProvider.ExecuteReader($"CALL GetFullIntern({(page - 1) * size},{size})");
        }

        public User GetUser(string eml, string pas)
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

        public bool CreateIntern(Intern model)
        {
            return DataProvider.ExecuteNonQuery($"CALL CreateIntern(" +
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
             $"'{model.Organization}', " +
             $"'{model.Department}')");
        }

        public bool CreateUser(User user, string pas)
        {
            // validation
            if (string.IsNullOrWhiteSpace(pas))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Email == user.Email))
                throw new AppException("Email \"" + user.Email + "\" is already taken");

            user.PasswordHash = BC.HashPassword(pas);

            return DataProvider.ExecuteNonQuery($"CALL CreateUser(" +
                $"'{user.FirstName}', " +
                $"'{user.LastName}', " +
                $"'{user.Email}', " +
                $"'{user.PasswordHash}')");
        }

        public bool CreateEvent(Event even)
        {
            //_context.Events.Add(even);
            //_context.SaveChanges();

            return DataProvider.ExecuteNonQuery($"CALL CreateEvent(" +
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


        /// <summary>
        /// GET STATE
        /// </summary>
        /// <returns></returns>
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
        public IList<Department> GetDepartments()
        {
            return _context.Departments.ToList();
        }
        public int GetInternCount()
        {
            return _context.Interns.Count();
        }

        public string GetTrainData(int id)
        {
            var data = DataProvider.ExecuteReader($"CALL GetTrainData('{id}')");

            if (data.Tables.Count < 1)
                return "Data not found!";

            return data.Tables[0].Rows[0]["TraData"].ToString();
        }

        public string GetEventJoined(int id)
        {
            var data = DataProvider.ExecuteReader($"CALL GetEventJoined('{id}')");

            if (data.Tables.Count < 1)
                return "Data not found!";

            return data.Tables[0].Rows[0]["Title"].ToString();
        }
    }
}