using FluentValidation;

namespace Domain.UserTickets.Commands
{
    public class SetTicketAgentCommandValidator : AbstractValidator<SetTicketAgentCommand>
    {
        public SetTicketAgentCommandValidator()
        {
            RuleFor(command => command.TicketId)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(command => command.AgentId)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }    
}
