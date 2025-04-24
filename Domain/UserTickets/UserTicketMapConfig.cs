using AutoMapper;
using DataAccessLayer.Models;
using Domain.UserTickets.Commands;

namespace Domain.UserTickets
{
    public class UserTicketMapConfig : Profile
    {
        public UserTicketMapConfig()
        {
            CreateMap<SetTicketAgentCommand, UserTicket>()
                .ForMember(ut => ut.TicketId, expression => expression.MapFrom(command => command.TicketId))
                .ForMember(ut => ut.UserId, expression => expression.MapFrom(command => command.AgentId))
                .ReverseMap();
        }
    }
}
