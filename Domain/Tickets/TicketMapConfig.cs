using AutoMapper;
using DataAccessLayer.Models;
using Domain.Tickets.Commands;
using Shared.Models.Ticket;

namespace Domain.Tickets
{
    public class TicketMapConfig : Profile
    {
        public TicketMapConfig()
        {
            CreateMap<CreateTicketCommand, Ticket>()
                .ForMember(t => t.UserId, expression => expression.MapFrom(create => create.UserId))
                .ForMember(t => t.IssueType, expression => expression.MapFrom(create => create.IssueType))
                .ForMember(t => t.Priority, expression => expression.MapFrom(create => create.Priority))
                .ForMember(t => t.Title, expression => expression.MapFrom(create => create.Title))
                .ForMember(t => t.Description, expression => expression.MapFrom(create => create.Description))
                .ForMember(t => t.Status, expression => expression.MapFrom(create => create.Status))
                .ReverseMap();

            CreateMap<Ticket, TicketDto>()
                .ForMember(dto => dto.Id, expression => expression.MapFrom(ticket => ticket.Id))
                .ForMember(dto => dto.CreatedAt, expression => expression.MapFrom(ticket => ticket.CreatedAt))
                .ForMember(dto => dto.ChatId, expression => expression.MapFrom(ticket => ticket.ChatId))
                .ForMember(dto => dto.IssueType, expression => expression.MapFrom(ticket => ticket.IssueType))
                .ForMember(dto => dto.Priority, expression => expression.MapFrom(ticket => ticket.Priority))
                .ForMember(dto => dto.Title, expression => expression.MapFrom(ticket => ticket.Title))
                .ForMember(dto => dto.Description, expression => expression.MapFrom(ticket => ticket.Description))
                .ForMember(dto => dto.Status, expression => expression.MapFrom(ticket => ticket.Status))
                .ForMember(dto => dto.UnreadMessages, expression => expression.MapFrom(ticket => ticket.UnreadMessages))
                .ReverseMap();
        }
    }
}
