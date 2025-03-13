namespace DataAccessLayer.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsClosed { get; set; }
    }
}
