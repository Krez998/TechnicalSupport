using FluentValidation;

namespace Domain.Messages.Commands
{
    public class CreateChatCommandValidator : AbstractValidator<CreateChatCommand>
    {
        public CreateChatCommandValidator()
        {
            RuleFor(command => command.TicketId)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }
}
