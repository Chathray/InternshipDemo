using AutoMapper;
using Internship.Data;
using InternshipApi.Models;

namespace InternshipApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AuthenticationModel, User>()
                   .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.RegiterEmail))
                   .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.RegiterFirstName))
                   .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.RegiterLastName));

            CreateMap<UserModel, User>();
            CreateMap<IndexModel, Intern>();
            CreateMap<CalendarModel, Event>();
        }
    }
}