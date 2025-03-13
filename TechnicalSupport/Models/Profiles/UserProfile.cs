using AutoMapper;
using DataAccessLayer.Models;
using TechnicalSupport.Models.UserModels;

namespace TechnicalSupport.Models.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserRequest, User>()
                .ForMember(user => user.Username, expression => expression
                .MapFrom(dto => dto.Login));

            CreateMap<User, CreateUserRequest>()
                .ForMember(dto => dto.Login, expression => expression
                .MapFrom(user => user.Username));

            CreateMap<UserData, User>();
            CreateMap<User, UserData>();

            CreateMap<AgentDto, User>();
            CreateMap<User, AgentDto>();
        }
    }
}
