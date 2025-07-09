using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Domain.Security
{
    public class JWTProvider : IJWTProvider
    {
        private readonly DomainSettings _settings;

        public JWTProvider(IOptions<DomainSettings> settings)
        {
            _settings = settings.Value;
        }

        public string GetToken(int userId, string login, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyval = _settings.Secret;
            var key = Encoding.ASCII.GetBytes(keyval);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserId", userId.ToString()),
                    new Claim(ClaimsIdentity.DefaultNameClaimType, login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role)                  
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var res = tokenHandler.WriteToken(token);
            return res;
        }
    }
}
