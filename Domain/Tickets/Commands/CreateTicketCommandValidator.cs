using FluentValidation;

namespace Domain.Tickets.Commands
{
    public class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
    {
        public CreateTicketCommandValidator()
        {
            RuleFor(command => command.UserId)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(command => command.IssueType)
                .NotNull()
                .NotEmpty();

            RuleFor(command => command.Priority)
                .NotNull()
                .NotEmpty();

            RuleFor(command => command.Title)
                .NotNull()
                .NotEmpty();

            RuleFor(command => command.Status)
                .NotNull()
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(7);
        }
    }
}
