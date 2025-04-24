using Domain.Users.Commands;
using Domain.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechnicalSupport.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Создает нового пользователя.
        /// </summary>
        /// <param name="createUserCommand">Данные для создания нового пользователя</param>
        /// <returns>Результат <see cref="OkResult"/>.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand createUserCommand)
        {
            await _mediator.Send(createUserCommand);
            return Ok();
        }

        /// <summary>
        /// Получает данные пользователя.
        /// </summary>
        /// <param name="id">ID пользователя.</param>
        /// <returns>Пользователь.</returns>
        [Authorize(Roles = "User,Agent,Admin")]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetUserQuery() { Id = id };
            var userDto = await _mediator.Send(query);
            return Ok(userDto);
        }

        /// <summary>
        /// Получает список всех исполнителей.
        /// </summary>
        /// <returns>Список исполнителей.</returns>
        [Authorize(Roles = "Agent,Admin")]
        [HttpGet("allAgents")]
        public async Task<IActionResult> GetAgents()
        {
            var query = new GetAgentsQuery();
            var agentsDto = await _mediator.Send(query);
            return Ok(agentsDto);
        }
    }
}
