namespace DataAccessLayer.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int SenderId { get; set; }
        public string? Content { get; set; }
    }
}
