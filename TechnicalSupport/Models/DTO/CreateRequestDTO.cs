namespace TechnicalSupport.Models.DTO
{
    public class CreateRequestDTO
    {
        public int UserId { get; set; }
        public string IssueType { get; set; }
        public string Priority { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }
}
