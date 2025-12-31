using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Models;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Message;
using Shared.Models.Ticket;
using Shared.Models.User;
using System.Threading.Tasks.Dataflow;

namespace Domain.Tickets.Queries
{
    public class GetTicketQueryHandler : IRequestHandler<GetTicketQuery, TicketDto>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public GetTicketQueryHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<TicketDto> Handle(GetTicketQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _dataContext.Tickets
                .AsNoTracking()
                .Include(t => t.Assignments)
                .ThenInclude(t => t.Assignee)
                .FirstOrDefaultAsync(ticket => ticket.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException($"Заявка с идентификатором {request.Id} не найдена.");

            var userFullName = await _dataContext.Users
                .AsNoTracking()
                .Where(user => user.Id == ticket.CreatedById)
                .Select(user => $"{user.LastName} {user.FirstName} {user.Patronymic}")
                .FirstOrDefaultAsync()
                ?? throw new NotFoundException($"Не найден регистратор заявки.");

            var messages = await _dataContext.Messages
                    .AsNoTracking()
                    .Where(m => m.ChatId == ticket.ChatId)
                    .ToListAsync(cancellationToken);

            var ticketDto = new TicketDto
            {
                Id = ticket.Id,
                Status = ticket.Status,
                UserId = ticket.CreatedById,
                UserFullName = userFullName,
                Title = ticket.Title,
                Description = ticket.Description,
                Agents = ticket.Assignments.Select(e => new AgentDto
                {
                    UserId = e.Assignee.Id,
                    FirstName = e.Assignee.FirstName,
                    LastName = e.Assignee.LastName,
                    Patronymic = e.Assignee.Patronymic
                }).ToList(),
                ChatId = ticket.ChatId,
                Messages = messages.Select(m => new MessageDto
                {
                    Id = m.Id,
                    SenderId = m.SenderId,
                    CreatedAt = m.CreatedAt,
                    Content = m.Content
                }).ToList()
            };

            return ticketDto;
        }
    }
}
