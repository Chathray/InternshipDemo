using AutoMapper;
using Internship.Application;

namespace Internship.Api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AuthenticationModel, UserModel>()
                   .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.RegiterEmail))
                   .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.RegiterFirstName))
                   .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.RegiterLastName));
        }
    }
}