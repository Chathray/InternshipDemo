using System.Data;

namespace Internship.Data
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        private readonly DataContext _context;
        private readonly DataProvider _provider;

        public EventRepository(DataContext context, DataProvider provider) : base(context)
        {
            _context = context;
            _provider = provider;
        }

        public DataTable GetEventsIntern()
        {
            return _provider.ExecuteReader($"CALL GetEventsJoined()");
        }

        public string GetJson()
        {
            var obj = _provider.ExecuteScalar($"CALL GetEventsJson()");
            return obj as string;
        }

        public bool InsertEvent(Event even)
        {
            //_context.Events.Add(even);
            //_context.SaveChanges();

            return _provider.ExecuteNonQuery($"CALL InsertEvent(" +
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
    }
}
