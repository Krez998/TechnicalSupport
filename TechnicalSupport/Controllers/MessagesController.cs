using Domain.Messages.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechnicalSupport.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Отправляет сообщение.
        /// </summary>
        /// <param name="sendMessageCommand">Данные для отправки сообщения.</param>
        /// <returns></returns>
        [Authorize(Roles = "User,Agent,Admin")]
        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] SendMessageCommand sendMessageCommand)
        {
            await _mediator.Send(sendMessageCommand);
            return Ok();
        }
    }
}
