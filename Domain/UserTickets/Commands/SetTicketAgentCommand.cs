using MediatR;

namespace Domain.UserTickets.Commands
{
    /// <summary>
    /// Команда, представляющая собой данные для назначения исполнителя на заявку.
    /// </summary>
    public class SetTicketAgentCommand : IRequest
    {
        public int TicketId { get; set; }
        public int AgentId { get; set; }
    }
}
