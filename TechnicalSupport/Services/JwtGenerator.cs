using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TechnicalSupport.Services
{
    public class JwtGenerator
    {
        public string Generate(string login, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyval = "78e0723ce1b86912be8ce3e4c9085b90904cdf44960475192ca4d1d50f875bb0";
            var key = Encoding.ASCII.GetBytes(keyval);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
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
