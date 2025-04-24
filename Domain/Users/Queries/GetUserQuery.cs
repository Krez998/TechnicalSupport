using MediatR;
using Shared.Models.User;

namespace Domain.Users.Queries
{
    /// <summary>
    /// Запрос, для получения данных пользователя по его идентификатору.
    /// </summary>
    public class GetUserQuery : IRequest<UserDto>
    {
        public int Id { get; set; }
    }
}
