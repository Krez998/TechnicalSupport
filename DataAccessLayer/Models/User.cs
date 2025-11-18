using Shared.Models.User;

namespace DataAccessLayer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string PasswordSalt { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Patronymic { get; set; } = default!;
        public Role Role { get; set; }


        public virtual ICollection<Ticket> CreatedTickets { get; set; } = default!;
        public virtual ICollection<TicketAssignment> AssignedTickets { get; set; } = default!;
        public virtual ICollection<Message> Messages { get; set; } = default!;
    }
}
