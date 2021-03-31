using Internship.Application;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Internship.Web
{
    public class EventViewModel
    {
        public EventViewModel() { }
        public EventViewModel(IReadOnlyList<EventTypeModel> eventtypes, IReadOnlyList<InternModel> interns)
        {
            EvenTypes = eventtypes;
            Interns = interns;
        }
         
        public IReadOnlyList<EventTypeModel> EvenTypes { get; set; }
        public IReadOnlyList<InternModel> Interns { get; set; }

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