using MediatR;

namespace Domain.Messages.Commands
{
    /// <summary>
    /// Команда создания нового чата.
    /// </summary>
    public class CreateChatCommand : IRequest<int>
    {
        /// <summary>
        /// Идентификатор заявки.
        /// </summary>
        public int TicketId { get; set; }
    }
}
