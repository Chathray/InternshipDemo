using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Dapper;
using System.Data;
using System.Linq;

namespace Internship.Infrastructure
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        private readonly DataContext _context;

        public EventRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public bool CheckOne(string title)
        {
            return _context.Events.SingleOrDefault(o => o.Title == title) is null;
        }

        public DataTable GetEventsIntern()
        {
            return _context.Database.GetDbConnection()
                .ExecReader($"CALL GetEventsJoined()");
        }

        public string GetJson()
        {
            var obj = _context.Database.GetDbConnection()
                .ExecuteScalar($"CALL GetEventsJson()");

            return obj as string;
        }

        public bool UpdateByTitle(Event model)
        {
            return _context.Database.GetDbConnection()
                .Execute($"CALL UpdateEventByTitle(" +
                        $"'{model.Title}', " +
                        $"'{model.Type}', " +
                        $"'{model.ClassName}', " +
                        $"'{model.Start}', " +
                        $"'{model.End}', " +
                        $"'{model.CreatedBy}', " +
                        $"'{model.GestsField}', " +
                        $"'{model.RepeatField}', " +
                        $"'{model.EventLocationLabel}', " +
                        $"'{model.EventDescriptionLabel}')") > 0;
        }
    }
}
