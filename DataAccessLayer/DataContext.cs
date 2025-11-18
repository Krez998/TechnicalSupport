using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models;
using DataAccessLayer.Configurations;

namespace DataAccessLayer
{
    public sealed class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketAssignment> TicketAssignments { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TicketConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TicketAssignmentConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MessageConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ChatConfiguration).Assembly);
        }
    }
}
