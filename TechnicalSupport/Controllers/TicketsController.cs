using Domain.Tickets.Commands;
using Domain.Tickets.Queries;
using Domain.UserTickets.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechnicalSupport.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Создает заявку.
        /// </summary>
        /// <param name="createTicketCommand">Данные для регистрации заявки.</param>
        /// <returns>Результат <see cref="OkResult"/>.</returns>
        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketCommand createTicketCommand)
        {
            await _mediator.Send(createTicketCommand);
            return Ok();
        }

        /// <summary>
        /// Меняет статус заявки.
        /// </summary>
        /// <param name="changeTicketStatusCommand">Данные для изменения статуса заявки.</param>
        /// <returns>Заявка.</returns>
        [Authorize(Roles = "Agent,Admin")]
        [HttpPatch("setStatus")]
        public async Task<IActionResult> ChangeTicketStatus([FromBody] ChangeTicketStatusCommand changeTicketStatusCommand)
        {
            var ticketDto = await _mediator.Send(changeTicketStatusCommand);
            return Ok(ticketDto);
        }

        /// <summary>
        /// Назначает исполнителя к заявке.
        /// </summary>
        /// <param name="setTicketAgentCommand">Информация о заявке и идентификаторе исполнителя, 
        /// который будет назначен.</param>
        /// <returns>Результат <see cref="OkResult"/>.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPatch("setAgent")]
        public async Task<IActionResult> SetTicketAgent([FromBody] SetTicketAgentCommand setTicketAgentCommand)
        {
            await _mediator.Send(setTicketAgentCommand);
            return Ok();
        }

        /// <summary>
        /// Получает заявку.
        /// </summary>
        /// <param name="id">ID заявки.</param>
        /// <returns>Заявка.</returns>
        [Authorize(Roles = "User,Agent,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetTicketQuery() { Id = id };
            var ticketDto = await _mediator.Send(query);
            return Ok(ticketDto);
        }

        /// <summary>
        /// Получает список всех заявок с учетом фильтров.
        /// </summary>
        /// <param name="getTicketsQuery">Параметры для фильтрации и получения заявок</param>
        /// <returns>Список заявок.</returns>
        [Authorize(Roles = "User,Agent,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetTicketsQuery getTicketsQuery)
        {
            var ticketsDto = await _mediator.Send(getTicketsQuery);
            return Ok(ticketsDto);
        }
    }
}
