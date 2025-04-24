using MediatR;
using Shared.Models.Ticket;

namespace Domain.Tickets.Queries
{
    /// <summary>
    /// Запрос, представляющий собой данные для получения всех заявок с учетом фильтров.
    /// </summary>
    public class GetTicketsQuery : IRequest<IEnumerable<TicketDto>>
    {
        public TicketStatus? Status { get; set; }
        public bool IsShowNotAssigned { get; set; }
    }
}
