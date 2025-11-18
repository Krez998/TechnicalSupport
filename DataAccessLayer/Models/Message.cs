namespace DataAccessLayer.Models
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? Content { get; set; }


        public int SenderId { get; set; }
        public virtual User Sender { get; set; } = default!;
        public int ChatId { get; set; }
        public virtual Chat Chat { get; set; } = default!;
    }
}
