using DataAccessLayer;
using Domain.Exceptions;
using Domain.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Accounts.Queries
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
    {
        private readonly DataContext _dataContext;
        private readonly IJWTProvider _jwtProvider;

        public LoginQueryHandler(DataContext dataContext, IJWTProvider jwtProvider)
        {
            _dataContext = dataContext;
            _jwtProvider = jwtProvider;
        }

        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _dataContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == request.Login && u.PasswordHash == request.Password, cancellationToken)
                ?? throw new AuthenticationException("Неправильно введены логин или пароль");

            return _jwtProvider.GetToken(user.Id, user.Username, user.Role.ToString());
        }
    }
}
