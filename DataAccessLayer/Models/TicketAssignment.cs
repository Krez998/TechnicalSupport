namespace DataAccessLayer.Models
{
    public class TicketAssignment
    {
        public DateTime AssignedAt { get; set; }


        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; } = default!;
        public int AssigneeId { get; set; }
        public virtual User Assignee { get; set; } = default!;
    }
}
