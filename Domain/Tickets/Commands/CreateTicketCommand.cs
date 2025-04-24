using MediatR;

namespace Domain.Tickets.Commands
{
    /// <summary>
    /// Команда, представляющая собой данные для регистрации заявки.
    /// </summary>
    public class CreateTicketCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public string IssueType { get; set; }
        public string Priority { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }
}
