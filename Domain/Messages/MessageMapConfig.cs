using AutoMapper;
using DataAccessLayer.Models;
using Domain.Messages.Commands;

namespace Domain.Messages
{
    public class MessageMapConfig : Profile
    {
        public MessageMapConfig()
        {
            CreateMap<SendMessageCommand, Message>()
                .ForMember(t => t.SenderId, expression => expression.MapFrom(create => create.SenderId))
                .ForMember(t => t.ChatId, expression => expression.MapFrom(create => create.ChatId))
                .ForMember(t => t.Content, expression => expression.MapFrom(create => create.Content))
                .ReverseMap();
        }
    }
}
