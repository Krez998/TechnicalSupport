namespace Shared.Models.Message
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Content { get; set; }
    }
}
