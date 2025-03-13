using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnicalSupport.Models.TicketModels;
using TechnicalSupport.Services;

namespace TechnicalSupport.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        /// <summary>
        /// Создает заявку
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _ticketService.Create(request, cancellationToken);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        /// <summary>
        /// Задает статус заявки
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [Authorize(Roles = "Executor,Admin")]
        [HttpPatch("setStatus/{id}")]
        public async Task<IActionResult> ChangeTicketStatus(int id, int status, CancellationToken cancellationToken)
        {
            try
            {
                await _ticketService.ChangeTicketStatus(id, status, cancellationToken);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Внутренняя ошибка сервера");
            }

            var ticket = _ticketService.Get(id, cancellationToken);
            return Ok(ticket);
        }

        /// <summary>
        /// Назначает исполнителя к заявке
        /// </summary>
        /// <param name="id">ID заявки</param>
        /// <param name="agentId">ID исполнителя</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPatch("setAgent/{id}")]
        public async Task<IActionResult> SetTicketExecutor(int id, [FromQuery] int agentId, CancellationToken cancellationToken)
        {
            try
            {
                await _ticketService.SetTicketExecutor(id, agentId, cancellationToken);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Внутренняя ошибка сервера");
            }

            return Ok();
        }

        /// <summary>
        /// Получает заявку
        /// </summary>
        /// <param name="id">ID заявки</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(Roles = "User,Executor,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket(int id, CancellationToken cancellationToken)
        {
            try
            {
                var ticket = await _ticketService.Get(id, cancellationToken);
                return Ok(ticket);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        /// <summary>
        /// Получает список всех заявок с учетом всех фильтров
        /// </summary>
        /// <param name="request">Входные данные</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(Roles = "User,Executor,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllTicketsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var requests = await _ticketService.GetAll(request, cancellationToken);
                return Ok(requests);
            }
            catch (Exception)
            {
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        /// <summary>
        /// Удаляет заявку (пока не используется)
        /// </summary>
        /// <param name="id">ID заявки</param>
        /// <param name="cancellationToken"></param>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _ticketService.Delete(id, cancellationToken);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        /// <summary>
        /// Отправляет сообщение
        /// </summary>
        /// <param name="content">текст сообщения</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(Roles = "User,Executor,Admin")]
        [HttpPost("sendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _ticketService.SendMessage(request, cancellationToken);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
    }
}
