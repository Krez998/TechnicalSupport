using MediatR;
using Shared.Models.Ticket;

namespace Domain.Tickets.Commands
{
    /// <summary>
    /// Команда, представляющая собой данные для изменения статуса заявки.
    /// </summary>
    public class ChangeTicketStatusCommand : IRequest<TicketDto>
    {
        public int Id { get; set; }
        public TicketStatus Status { get; set; }
    }
}
