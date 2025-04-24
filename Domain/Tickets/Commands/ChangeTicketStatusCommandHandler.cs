using AutoMapper;
using DataAccessLayer;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Ticket;

namespace Domain.Tickets.Commands
{
    public class ChangeTicketStatusCommandHandler : IRequestHandler<ChangeTicketStatusCommand, TicketDto>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ChangeTicketStatusCommandHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<TicketDto> Handle(ChangeTicketStatusCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _dataContext.Tickets
                .AsNoTracking()
                .FirstOrDefaultAsync(ticket => ticket.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException($"Заявка с идентификатором {request.Id} не найдена");

            ticket.Status = request.Status;
            await _dataContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<TicketDto>(ticket);
        }
    }
}
