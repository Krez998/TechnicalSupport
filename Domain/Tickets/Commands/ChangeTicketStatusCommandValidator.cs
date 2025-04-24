using FluentValidation;

namespace Domain.Tickets.Commands
{
    public class ChangeTicketStatusCommandValidator : AbstractValidator<ChangeTicketStatusCommand>
    {
        public ChangeTicketStatusCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(command => command.Status)
                .NotNull()
                .IsInEnum();
        }
    }
}
