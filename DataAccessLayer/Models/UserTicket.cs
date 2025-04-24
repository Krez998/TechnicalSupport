namespace DataAccessLayer.Models
{
    public class UserTicket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
