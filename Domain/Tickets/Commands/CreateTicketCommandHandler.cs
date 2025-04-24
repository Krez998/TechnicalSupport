using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Models;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Tickets.Commands
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, int>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public CreateTicketCommandHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            if(!await _dataContext.Users.AnyAsync(t => t.Id == request.UserId, cancellationToken)) 
                throw new NotFoundException($"Пользователя с идентификатором {request.UserId} не существует.");
 
            var ticket = _mapper.Map<Ticket>(request);
            await _dataContext.Tickets.AddAsync(ticket, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);
            return ticket.Id;
        }
    }
}
