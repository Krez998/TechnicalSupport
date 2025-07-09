using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Models;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Domain.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly PasswordHashingSettings _hashingSettings;

        public CreateUserCommandHandler(DataContext dataContext, IMapper mapper, IOptions<PasswordHashingSettings> hashingSettings)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _hashingSettings = hashingSettings.Value;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            string hashedPassword;
            string salt;

            if (_hashingSettings.Algorithm == "BCrypt")
            {
                hashedPassword = BCrypt.Net.BCrypt.HashPassword(
                    request.Password,
                    _hashingSettings.WorkFactor);
                salt = string.Empty;
            }
            else
            {
                byte[] saltBytes = new byte[_hashingSettings.SaltSize];
                using (var rng = RandomNumberGenerator.Create())
                    rng.GetBytes(saltBytes);

                salt = Convert.ToBase64String(saltBytes);

                hashedPassword = Convert.ToBase64String(
                    KeyDerivation.Pbkdf2(
                        password: request.Password,
                        salt: saltBytes,
                        prf: KeyDerivationPrf.HMACSHA512,
                        iterationCount: _hashingSettings.Iterations,
                        numBytesRequested: _hashingSettings.KeySize
                    )
                );
            }

            var user = _mapper.Map<User>(request);
            user.PasswordHash = hashedPassword;
            user.PasswordSalt = salt;

            await _dataContext.Users.AddAsync(user, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);
        }
    }
}
