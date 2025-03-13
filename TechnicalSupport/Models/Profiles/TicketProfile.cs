using AutoMapper;
using DataAccessLayer.Models;
using TechnicalSupport.Models.TicketModels;

namespace TechnicalSupport.Models.Profiles
{
    public class TicketProfile: Profile
    {
        public TicketProfile()
        {
            CreateMap<CreateTicketRequest, Ticket>();
            CreateMap<Ticket, CreateTicketRequest>();

            CreateMap<TicketDto, Ticket>();
            CreateMap<Ticket, TicketDto>();

            CreateMap<SendMessageRequest, Message>();
            CreateMap<Message, SendMessageRequest>();
        }
    }
}
