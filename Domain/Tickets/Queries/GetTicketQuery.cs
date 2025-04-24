using MediatR;
using Shared.Models.Ticket;

namespace Domain.Tickets.Queries
{
    /// <summary>
    /// Запрос, представляющий собой данные для получения заявки.
    /// </summary>
    public class GetTicketQuery : IRequest<TicketDto>
    {
        public int Id { get; set; }
    }
}
