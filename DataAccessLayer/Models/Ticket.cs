using Shared.Models.Ticket;

namespace DataAccessLayer.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string IssueType { get; set; } = default!;
        public string Priority { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public TicketStatus Status { get; set; }
        public int UnreadMessages { get; set; }


        public int CreatedById { get; set; }
        public virtual User CreatedBy { get; set; } = default!;
        public int? ChatId { get; set; }
        public virtual Chat Chat { get; set; } = default!;
        public virtual ICollection<TicketAssignment> Assignments { get; set; } = default!;
    }
}
