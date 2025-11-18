using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configurations
{
    internal class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(ticket => ticket.Id);

            builder.HasOne(ticket => ticket.CreatedBy)
                .WithMany(user => user.CreatedTickets)
                .HasForeignKey(ticket => ticket.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ticket => ticket.Chat)
                .WithOne(chat => chat.Ticket)
                .HasForeignKey<Ticket>(ticket => ticket.ChatId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(ticket => ticket.CreatedById);

            builder.HasIndex(ticket => ticket.Status);

            builder.HasIndex(ticket => ticket.ChatId).IsUnique();
        }
    }
}
