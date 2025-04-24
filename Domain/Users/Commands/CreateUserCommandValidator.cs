using FluentValidation;

namespace Domain.Users.Commands
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.Login)
                .NotNull()
                .NotEmpty()
                .Length(3, 20)
                .Matches("^[a-zA-Z0-9_.-]*$").WithMessage("Логин может содержать только буквы, цифры, точки, подчеркивания и дефисы.");

            RuleFor(command => command.Password)
                .NotNull()
                .NotEmpty()
                .Length(8, 100)
                .Matches(@"[A-Z]").WithMessage("Пароль должен содержать хотя бы одну заглавную букву.")
                .Matches(@"[a-z]").WithMessage("Пароль должен содержать хотя бы одну строчную букву.")
                .Matches(@"[0-9]").WithMessage("Пароль должен содержать хотя бы одну цифру.")
                .Matches(@"[\W_]").WithMessage("Пароль должен содержать хотя бы один специальный символ.");

            RuleFor(command => command.FirstName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .Matches(@"^[А-Яа-яЁёA-Za-z\s]+$").WithMessage("Имя может содержать только буквы и пробелы.")
                .Matches(@"^[^\d]*$").WithMessage("Имя не должно содержать цифр.");

            RuleFor(command => command.LastName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .Matches(@"^[А-Яа-яЁёA-Za-z\s]+$").WithMessage("Имя может содержать только буквы и пробелы.")
                .Matches(@"^[^\d]*$").WithMessage("Имя не должно содержать цифр.");

            RuleFor(command => command.Patronymic)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .Matches(@"^[А-Яа-яЁёA-Za-z\s]+$").WithMessage("Имя может содержать только буквы и пробелы.")
                .Matches(@"^[^\d]*$").WithMessage("Имя не должно содержать цифр.");

            RuleFor(command => command.Role)
                .NotNull()
                .IsInEnum();
        }
    }
}
