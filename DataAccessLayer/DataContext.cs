using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public sealed class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<UserTicket> UserTickets { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTicket>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.UserTickets)
                .HasForeignKey(ut => ut.UserId);

            modelBuilder.Entity<UserTicket>()
                .HasOne(ut => ut.Ticket)
                .WithMany(t => t.UserTickets)
                .HasForeignKey(ut => ut.TicketId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId);
        }
    }
}
