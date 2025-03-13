public class SendMessageRequest
{
    public int SenderId { get; set; }
    public int ChatId { get; set; }
    public string? Content { get; set; }
}