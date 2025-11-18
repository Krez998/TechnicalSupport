using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Models;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Messages.Commands
{
    public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, int>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public CreateChatCommandHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _dataContext.Tickets
                .FirstOrDefaultAsync(t => t.Id == request.TicketId, cancellationToken)
                ?? throw new NotFoundException($"Заявка с id {request.TicketId} не найдена!");

            var existingChat = await _dataContext.Chats
                .FirstOrDefaultAsync(c => c.Ticket.Id == request.TicketId, cancellationToken);

            if (existingChat != null)
                return existingChat.Id;

            var newChat = new Chat
            {
                CreatedAt = DateTime.Now.ToUniversalTime(),
                UpdatedAt = DateTime.Now.ToUniversalTime(),
                IsClosed = false,
                Ticket = ticket
            };

            await _dataContext.Chats.AddAsync(newChat, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);
            return newChat.Id;
        }
    }
}
