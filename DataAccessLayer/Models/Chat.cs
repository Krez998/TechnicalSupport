namespace DataAccessLayer.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsClosed { get; set; }


        public virtual Ticket Ticket { get; set; } = default!;
        public virtual ICollection<Message> Messages { get; set; } = default!;
    }
}
