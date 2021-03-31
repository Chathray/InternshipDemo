using Internship.Application;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Internship.Web
{
    public class CalendarViewModel
    {
        public CalendarViewModel() { }
        public CalendarViewModel(IList<EventTypeModel> eventtypes, IList<InternModel> interns)
        {
            EvenTypes = eventtypes;
            Interns = interns;
        }
         
        public IList<EventTypeModel> EvenTypes { get; set; }
        public IList<InternModel> Interns { get; set; }

        public string Creator { get; set; }

        public string Title { get; set; }
        public string Type { get; set; }
        public string Deadline { get; set; }
        public string RepeatField { get; set; }
        public string GestsField { get; set; }
        public string EventLocationLabel { get; set; }
        public string EventDescriptionLabel { get; set; }

        public string GetWhitelist()
        {
            var json = JsonConvert.SerializeObject(Interns);
            return json;
        }
    }
}