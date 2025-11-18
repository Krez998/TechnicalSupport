using AutoMapper;
using DataAccessLayer.Models;
using Domain.UserTickets.Commands;

namespace Domain.UserTickets
{
    public class TicketAssignmentMapConfig : Profile
    {
        public TicketAssignmentMapConfig()
        {
            CreateMap<SetTicketAgentCommand, TicketAssignment>()
                .ForMember(ut => ut.TicketId, expression => expression.MapFrom(command => command.TicketId))
                .ForMember(ut => ut.AssigneeId, expression => expression.MapFrom(command => command.AgentId))
                .ReverseMap();
        }
    }
}
