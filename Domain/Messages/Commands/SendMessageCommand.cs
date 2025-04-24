using MediatR;

namespace Domain.Messages.Commands
{
    /// <summary>
    /// Команда, содержащая текст сообщения и прочие данные
    /// </summary>
    public class SendMessageCommand : IRequest
    {
        public int SenderId { get; set; }
        public int ChatId { get; set; }
        public string? Content { get; set; }
    }
}
