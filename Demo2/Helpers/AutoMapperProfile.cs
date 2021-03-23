using AutoMapper;
using WebApplication.Models;

namespace WebApplication
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AuthenticationModel, User>();
            CreateMap<IndexModel, Intern>();
            CreateMap<CalendarModel, Event>();
        }
    }
}
