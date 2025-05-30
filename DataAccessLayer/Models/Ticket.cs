﻿using Shared.Models.Ticket;

namespace DataAccessLayer.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<UserTicket> UserTickets { get; set; }
        //public int UserId { get; set; }
        //public int? AgentId { get; set; }
        public int? ChatId { get; set; }
        public string IssueType { get; set; }
        public string Priority { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TicketStatus Status { get; set; }
        public int UnreadMessages { get; set; }
    }
}
