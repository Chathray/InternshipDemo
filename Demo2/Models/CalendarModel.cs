using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class CalendarModel
    {
        public CalendarModel() { }
        public CalendarModel(IList<EventType> a, IList<Intern> b)
        {
            EvenTypes = a;
            Intenrs = b;
        }
        public IList<EventType> EvenTypes { get; set; }
        public IList<Intern> Intenrs { get; set; }

        public string Creator { get; set; }


        public string Title { get; set; }
        public string Type { get; set; }
        public string Deadline { get; set; }
        public string RepeatField { get; set; }
        public string GestsField { get; set; }
        public string EventLocationLabel { get; set; }
        public string EventDescriptionLabel { get; set; }


        public string GetUserList()
        {
            return JsonConvert.SerializeObject(Intenrs);
        }
    }
}