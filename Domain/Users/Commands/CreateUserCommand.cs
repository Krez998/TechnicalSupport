using MediatR;
using Shared.Models.User;

namespace Domain.Users.Commands
{
    /// <summary>
    /// Команда, представляющая собой данные для добавления нового пользователя.
    /// </summary>
    public class CreateUserCommand : IRequest
    {
        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Patronymic { get; set; } = default!;
        public Role Role { get; set; }
    }
}
