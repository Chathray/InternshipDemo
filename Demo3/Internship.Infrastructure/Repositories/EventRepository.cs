using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Dapper;
using System.Data;

namespace Internship.Infrastructure
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        private readonly DataContext _context;

        public EventRepository(DataContext context) : base(context)
        {
            _context = context;
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

        public bool InsertEvent(Event even)
        {
            _context.Events.Add(even);
            return _context.SaveChanges() > 0;
        }
    }
}
