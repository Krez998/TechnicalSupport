using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using System.Threading;
using TechnicalSupport.Data;
using TechnicalSupport.Models.DTO;
using TechnicalSupport.Models.Response;
using TechnicalSupport.Services;

namespace TechnicalSupport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        public MyDataContext _dataContext;
        private readonly JwtGenerator _jwtGenerator = new();

        public AccountsController(MyDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        public async Task<IActionResult> Login(string login, string password)
        {
            try
            {
                var user = _dataContext.Users.FirstOrDefault(u => u.Login == login && u.Password == password);

                if (user == null)
                    return Unauthorized("Пользователь не найден");

                var token = _jwtGenerator.Generate(user.Login, user.Role.ToString());

                return Ok(new UserDataResponse()
                {
                    Token = token,
                    User = new UserAuthDataResponse()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Patronymic = user.Patronymic,
                        Role = user.Role
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, "Произошла ошибка на сервере.");
            }
        }

        [Authorize(Roles = "User,Executor")]
        [HttpGet("/me")]
        public ActionResult MEEEE(string login, string password)
        {
            return Ok(HttpContext.User.Claims?
                .FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType)?.Value);
        }
    }
}
