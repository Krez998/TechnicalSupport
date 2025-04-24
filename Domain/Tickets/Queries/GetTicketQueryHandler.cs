﻿using AutoMapper;
using DataAccessLayer;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Message;
using Shared.Models.Ticket;
using Shared.Models.User;

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
                .Include(t => t.UserTickets)
                .ThenInclude(t => t.User)
                .FirstOrDefaultAsync(ticket => ticket.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException($"Заявка с идентификатором {request.Id} не найдена");

            var messages = await _dataContext.Messages
                .AsNoTracking()
                .Where(m => m.ChatId == ticket.ChatId)
                .ToListAsync(cancellationToken);

            var ticketDto = new TicketDto
            {
                Id = ticket.Id,
                UserId = ticket.UserId,
                Title = ticket.Title,
                Description = ticket.Description,
                Agents = ticket.UserTickets.Select(e => new AgentDto
                {
                    UserId = e.User.Id,
                    FirstName = e.User.FirstName,
                    LastName = e.User.LastName,
                    Patronymic = e.User.Patronymic
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
