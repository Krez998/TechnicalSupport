using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnicalSupport.Models.UserModels;
using TechnicalSupport.Services;

namespace TechnicalSupport.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Получает пользователя
        /// </summary>
        /// <param name="id">ID пользователя</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUser(int id, CancellationToken cancellationToken)
        {
            var user = await _userService.Get(id, cancellationToken);
            if (user is null) return NotFound("Такого пользователя не существует");
            return Ok(user);
        }

        /// <summary>
        /// Получает список всех исполнителей
        /// </summary>
        /// <returns></returns>
        [HttpGet("allAgents")]
        public async Task<IActionResult> GetAgents(CancellationToken cancellationToken)
        {
            await _userService.GetAgents(cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Создает нового пользователя в системе
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
        {
            await _userService.Create(request, cancellationToken);
            return Ok();
        }
    }
}
