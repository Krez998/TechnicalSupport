﻿using Shared.Models.User;

namespace DataAccessLayer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public Role Role { get; set; }
        public ICollection<UserTicket> UserTickets { get; set; }
    }
}
