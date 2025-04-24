using Domain.Accounts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TechnicalSupport.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Аутентифицирует пользователя.
        /// </summary>
        /// <param name="loginQuery">Данные, необходимые для аутентификации пользователя в системе.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("/login")]
        public async Task<ActionResult<string>> Login([FromQuery] LoginQuery loginQuery)
        {
            try
            {
                var jwtToken = await _mediator.Send(loginQuery);
                return Ok(jwtToken);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Получает UserID пользователя.
        /// </summary>
        /// <returns>userId</returns>
        [Authorize(Roles = "User,Agent,Admin")]
        [HttpGet("/userId")]
        public ActionResult<string> GetUserId()
        {
            var userId = HttpContext.User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return NotFound("UserID не найден.");
            }

            return Ok(userId);
        }

        /// <summary>
        /// Получает имя пользователя.
        /// </summary>
        /// <returns>userName</returns>
        [Authorize]
        [HttpGet("/username")]
        public ActionResult<string> GetUserName()
        {
            var userName = HttpContext.User.Identity?.Name;

            if (string.IsNullOrEmpty(userName))
            {
                return NotFound("Имя пользователя не найдено.");
            }

            return Ok(userName);
        }
    }
}
