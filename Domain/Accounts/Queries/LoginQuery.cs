using MediatR;

namespace Domain.Accounts.Queries
{
    /// <summary>
    /// Запрос, содержащий логин и пароль для аутентификации пользователя в системе.
    /// </summary>
    public class LoginQuery : IRequest<string>
    {
        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
