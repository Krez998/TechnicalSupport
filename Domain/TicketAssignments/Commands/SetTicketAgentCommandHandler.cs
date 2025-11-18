using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Models;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Ticket;

namespace Domain.UserTickets.Commands
{
    public class SetTicketAgentCommandHandler : IRequestHandler<SetTicketAgentCommand>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public SetTicketAgentCommandHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task Handle(SetTicketAgentCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _dataContext.Tickets
                .AsNoTracking()
                .FirstOrDefaultAsync(ticket => ticket.Id == request.TicketId, cancellationToken) 
                ?? throw new NotFoundException($"Заявка с идентификатором {request.TicketId} не найдена.");

            var agent = await _dataContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Id == request.AgentId, cancellationToken)
                ?? throw new NotFoundException($"Пользователя с идентификатором {request.AgentId} не существует.");

            ticket.Status = TicketStatus.Assigned;
            var ticketAssignment = _mapper.Map<TicketAssignment>(request);
            await _dataContext.TicketAssignments.AddAsync(ticketAssignment, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);
        }
    }
}
