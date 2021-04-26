using AutoMapper;
using Internship.Application;

namespace Internship.Api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterModel, UserModel>();
        }
    }
}