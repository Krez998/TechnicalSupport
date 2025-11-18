using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configurations
{
    internal class TicketAssignmentConfiguration : IEntityTypeConfiguration<TicketAssignment>
    {
        public void Configure(EntityTypeBuilder<TicketAssignment> builder)
        {
            builder.HasKey(assignment => new { assignment.TicketId, assignment.AssigneeId });

            builder.HasOne(assignment => assignment.Ticket)
                .WithMany(ticket => ticket.Assignments)
                .HasForeignKey(assignment => assignment.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(assignment => assignment.Assignee)
                .WithMany(user => user.AssignedTickets)
                .HasForeignKey(assignment => assignment.AssigneeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(assignment => assignment.AssigneeId);
        }
    }
}
