using MediatR;
using Shared.Models.User;

namespace Domain.Users.Commands
{
    /// <summary>
    /// Команда, представляющая собой данные для добавления нового пользователя.
    /// </summary>
    public class CreateUserCommand : IRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public Role Role { get; set; }
    }
}
