using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using TechnicalSupport.Models.UserModels;

namespace TechnicalSupport.Services
{
    public class AccountService : IAccountService
    {
        private readonly DataContext _dataContext;
        private readonly IJwtGenerator _jwtGenerator;

        public AccountService(DataContext dataContext, IJwtGenerator jwtGenerator)
        {
            _dataContext = dataContext;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<UserDataDto> Login(string login, string password, CancellationToken cancellationToken)
        {
            var user = await _dataContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == login && u.PasswordHash == password, cancellationToken)
                ?? throw new KeyNotFoundException("Неправильно введены логин или пароль");

            var token = _jwtGenerator.Generate(user.Username, user.Role.ToString());

            return new UserDataDto()
            {
                Token = token,
                User = new UserData()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Patronymic = user.Patronymic,
                    Role = (Models.Role)user.Role
                }
            };
        }
    }
}
