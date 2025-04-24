using AutoMapper;
using DataAccessLayer.Models;
using Domain.Users.Commands;
using Shared.Models.User;

namespace Domain.Users
{
    public class UserMapConfig : Profile
    {
        public UserMapConfig()
        {
            CreateMap<CreateUserCommand, User>()
                .ForMember(user => user.Username, expression => expression.MapFrom(create => create.Login))
                .ForMember(user => user.PasswordHash, expression => expression.MapFrom(create => create.Password))
                .ForMember(user => user.FirstName, expression => expression.MapFrom(create => create.FirstName))
                .ForMember(user => user.LastName, expression => expression.MapFrom(create => create.LastName))
                .ForMember(user => user.Patronymic, expression => expression.MapFrom(create => create.Patronymic))
                .ForMember(user => user.Role, expression => expression.MapFrom(create => create.Role))
                .ReverseMap();

            CreateMap<UserDto, User>()
                .ForMember(user => user.Id, expression => expression.MapFrom(dto => dto.Id))
                .ForMember(user => user.FirstName, expression => expression.MapFrom(dto => dto.FirstName))
                .ForMember(user => user.LastName, expression => expression.MapFrom(dto => dto.LastName))
                .ForMember(user => user.Patronymic, expression => expression.MapFrom(dto => dto.Patronymic))
                .ForMember(user => user.Role, expression => expression.MapFrom(dto => dto.Role))
                .ReverseMap();

            CreateMap<AgentDto, User>()
                .ForMember(user => user.Id, expression => expression.MapFrom(dto => dto.UserId))
                .ForMember(user => user.FirstName, expression => expression.MapFrom(dto => dto.FirstName))
                .ForMember(user => user.LastName, expression => expression.MapFrom(dto => dto.LastName))
                .ForMember(user => user.Patronymic, expression => expression.MapFrom(dto => dto.Patronymic))
                .ReverseMap();
        }
    }
}
