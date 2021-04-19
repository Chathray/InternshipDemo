using AutoMapper;
using Internship.Application;

namespace Internship.Web
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Normal
            CreateMap<IndexViewModel, InternModel>();
            CreateMap<CalendarViewModel, EventModel>();
            CreateMap<PointViewModel, PointModel>();
            CreateMap<QuestionViewModel, QuestionModel>();

            CreateMap<UserViewModel, UserModel>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.RegiterPassword))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.RegiterEmail))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.RegiterFirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.RegiterLastName));


            // Reverse
            CreateMap<SettingsViewModel, UserModel>().ReverseMap();
        }
    }
}