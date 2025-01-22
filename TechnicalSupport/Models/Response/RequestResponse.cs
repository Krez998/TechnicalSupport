namespace TechnicalSupport.Models.Response
{
    public class RequestResponse
    {
        public int Id { get; set; }
        public string ExecutorFullName { get; set; }
        public int? ExecutorId { get; set; }
        public int UserId { get; set; }
        public string IssueType { get; set; }
        public string Priority { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public RequestStatus Status { get; set; }
        public int UnreadMessages { get; set; }
    }
}
