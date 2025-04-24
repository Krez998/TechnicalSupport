using FluentValidation;

namespace Domain.Messages.Commands
{
    public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
    {
        public SendMessageCommandValidator()
        {
            RuleFor(command => command.SenderId)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(command => command.ChatId)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(command => command.Content)
                .NotNull()
                .NotEmpty();
        }
    }
}
