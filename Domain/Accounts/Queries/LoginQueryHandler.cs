using DataAccessLayer;
using Domain.Exceptions;
using Domain.Security;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Domain.Accounts.Queries
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
    {
        private readonly DataContext _dataContext;
        private readonly IJWTProvider _jwtProvider;
        private readonly PasswordHashingSettings _hashingSettings;

        public LoginQueryHandler(DataContext dataContext, IJWTProvider jwtProvider, IOptions<PasswordHashingSettings> hashingSettings)
        {
            _dataContext = dataContext;
            _jwtProvider = jwtProvider;
            _hashingSettings = hashingSettings.Value;
        }

        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            //var user = await _dataContext.Users
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync(u => u.Username == request.Login && u.PasswordHash == request.Password, cancellationToken)
            //    ?? throw new AuthenticationException("Неправильно введены логин или пароль");

            //return _jwtProvider.GetToken(user.Id, user.Username, user.Role.ToString());

            var user = await _dataContext.Users
                .FirstOrDefaultAsync(u => u.Username == request.Login, cancellationToken)
                ?? throw new AuthenticationException("Пользователь не найден.");

            if (!VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
                throw new AuthenticationException("Неправильный пароль.");

            return _jwtProvider.GetToken(user.Id, user.Username, user.Role.ToString());
        }

        private bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {           
            try
            {
                if (_hashingSettings.Algorithm == "BCrypt")
                    return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHash);

                // PBKDF2 по умолчанию
                byte[] salt = Convert.FromBase64String(storedSalt);
                byte[] hashToVerify = KeyDerivation.Pbkdf2(
                    password: enteredPassword,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: _hashingSettings.Iterations,
                    numBytesRequested: _hashingSettings.KeySize
                );
                return Convert.ToBase64String(hashToVerify) == storedHash;
            }
            catch
            {
                return false;
            }
        }
    }
}
